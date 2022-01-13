using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using BO;
using BlApi;
using System.ComponentModel;

namespace PL
{
    public partial class Drone : Window
    {
        BlApi.IBL ibl;
        BO.Drone drone;
        bool S = false;
        BackgroundWorker worker;
        public event EventHandler<RoutedEventArgs> needToRefreshScreenEvent;
        private RoutedEventArgs info;
        public Drone(BlApi.IBL V)
        {
            InitializeComponent();
            DataContext = this;
            drone = new BO.Drone();
            A.Visibility = Visibility.Visible;
            B.Visibility = Visibility.Visible;
            AddButton.Visibility = Visibility.Visible;
            CloseButton.Visibility = Visibility.Visible;
            TextBlock1.Visibility = Visibility.Visible;
            TextBlock2.Visibility = Visibility.Visible;
            model.Visibility = Visibility.Visible;
            id.Visibility = Visibility.Visible;
            StationID.Visibility = Visibility.Visible;
            StationText.Visibility = Visibility.Visible;
            CheckText.Visibility = Visibility.Hidden;
            IdText.Visibility = Visibility.Hidden;
            ModelText.Visibility = Visibility.Hidden;
            LongitudeText1.Visibility = Visibility.Hidden;
            LongitudeText2.Visibility = Visibility.Hidden;
            LattitudeText1.Visibility = Visibility.Hidden;
            LattitudeText2.Visibility = Visibility.Hidden;
            OkButton.Visibility = Visibility.Hidden;
            WeightText1.Visibility = Visibility.Hidden;
            WeightText2.Visibility = Visibility.Hidden;
            BatteryText1.Visibility = Visibility.Hidden;
            BatteryText2.Visibility = Visibility.Hidden;
            StatusText1.Visibility = Visibility.Hidden;
            StatusText2.Visibility = Visibility.Hidden;
            ChangeButton1.Visibility = Visibility.Hidden;
            ChangeButton2.Visibility = Visibility.Hidden;
            ChargingTime.Visibility = Visibility.Hidden;
            Simulator.Visibility = Visibility.Hidden;
            drone.status = BO.DroneStatus.free;
            B.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            ibl = V;
        }
        public Drone(BlApi.IBL V, BO.Drone _drone)
        {
            InitializeComponent();
            DataContext = this;
            worker = new BackgroundWorker();
            worker.DoWork += StartWork;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += ScreenWork;
            worker.RunWorkerCompleted += CompletedWork;
            CheckText.Visibility = Visibility.Hidden;
            ibl = V;
            IdText.Text = "" + _drone.id;
            ModelText.Text = _drone.model;
            LongitudeText2.Text = "" + _drone.location.longitude;
            LattitudeText2.Text = "" + _drone.location.latitude;
            WeightText2.Text = "" + _drone.maxWeight;
            BatteryText2.Text = "" + _drone.battery;
            StatusText2.Text = "" + _drone.status;
            drone = _drone;
            if (drone.status == BO.DroneStatus.free){ ChangeButton1.Content = "Assign to parcel"; }
            //if (drone.parcel != null && ibl.displayParcel(drone.parcel.id).collectedParcelTime == DateTime.MinValue) { ChangeButton1.Content = "Clollect parcel";  }
            else
            {
                //if (ibl.displayParcel(drone.parcel.id).providedParcelTime == DateTime.MinValue) { ChangeButton1.Content = "Provide parcel"; }
            }
            if (drone.status == BO.DroneStatus.delivery) { ChangeButton2.Visibility = Visibility.Hidden; ChargingTime.Visibility = Visibility.Hidden; }
            if (drone.status == BO.DroneStatus.free) { ChangeButton2.Content = "Send to charge"; ChargingTime.Visibility = Visibility.Hidden; }
            if (drone.status == BO.DroneStatus.matance) { ChangeButton2.Content = "Release from charge"; }
        }
        private void StartWork(object sender, DoWorkEventArgs e)
        {
            Action RE = () => worker.ReportProgress(1);
            ibl.ActSimulator(drone.id, RE, () => S);
        }
        private void ScreenWork(object sender, ProgressChangedEventArgs e)
        {
            drone = ibl.displayDrone(drone.id);
            DataContext = drone;
            needToRefreshScreenEvent?.Invoke(this, info);
        }
        private void CompletedWork(object sender, RunWorkerCompletedEventArgs e)
        {
            if (S) { Simulator.Content = "Simulator"; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Drone D = new BO.Drone();
            D.model = model.Text;
            if(id.Text != "")D.id = Convert.ToInt32(id.Text);
            else { D.id = 0; }
            D.status = (BO.DroneStatus)0;
            switch (B.SelectedIndex)
            {
                case 0:
                    D.maxWeight = (BO.WeightCategories)0;
                    break;
                case 1:
                    D.maxWeight = (BO.WeightCategories)1;
                    break;
                case 2:
                    D.maxWeight = (BO.WeightCategories)2;
                    break;
                default:
                    break;
            }
            ibl.AddDrone(D, int.Parse(StationID.Text));
            DroneWindow.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e) {/*update on DroneList.xml*/ DroneWindow.Close(); }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            drone.model = ModelText.Text;
            ibl.UpdateDrone(drone);
            DroneWindow.Close();
        }
        private void ChangeButton1_Click(object sender, RoutedEventArgs e)
        {
            if (drone.status == BO.DroneStatus.free)
            {
                BO.Parcel parcel = ibl.assignParcelToDrone(drone.id);
                BO.Location send = ibl.displayCustomer(parcel.delivered.id).location;
                BO.Location recieved = ibl.displayCustomer(parcel.getted.id).location;
                drone.parcel = new ParcelInTransfer() { id = parcel.Id, sender = parcel.delivered, getter = parcel.getted, priority = parcel.priority, status = false, collection = send, target = recieved, distance = ibl.DistanceTo(send.latitude, send.longitude, recieved.latitude, recieved.longitude), weight = parcel.weight };
                drone.status = BO.DroneStatus.delivery;
            }
            else if(drone.status == BO.DroneStatus.delivery)
            {
                if(ibl.displayParcel(drone.parcel.id).collectedParcelTime == DateTime.MinValue)
                {
                    ibl.collectParcelByDrone(drone.id);
                }
                else if(ibl.displayParcel(drone.parcel.id).providedParcelTime == DateTime.MinValue)
                {
                    ibl.provideParcelByDrone(drone.id);
                    drone.status = BO.DroneStatus.free;
                }
            }
        }

        private void ChangeButton2_Click(object sender, RoutedEventArgs e)
        {
            double D;
            if (!double.TryParse(ChargingTime.Text, out D)) { D = 0; }
            if (drone.status == BO.DroneStatus.free) { ibl.sendDroneToCharging(drone.id); drone.status = BO.DroneStatus.matance; ChangeButton2.Visibility = Visibility.Hidden; CheckText.Visibility = Visibility.Visible; }
            else if (drone.status == BO.DroneStatus.matance) { ibl.releaseDroneFromCharging(drone.id, D); drone.status = BO.DroneStatus.free; ChangeButton2.Visibility = Visibility.Hidden; ChargingTime.Visibility = Visibility.Hidden; CheckText.Visibility = Visibility.Visible; }
        }
        private void HideUpdate()
        {
            ModelText.IsReadOnly = true;
            OkButton.Visibility = Visibility.Hidden;
            ChangeButton1.Visibility = Visibility.Hidden;
            ChangeButton2.Visibility = Visibility.Hidden;
            ChargingTime.Visibility = Visibility.Hidden;
        }
        private void DisplayUpdate()
        {
            ModelText.IsReadOnly = false;
            OkButton.Visibility = Visibility.Visible;
            ChangeButton1.Visibility = Visibility.Visible;
            ChangeButton2.Visibility = Visibility.Visible;
            ChargingTime.Visibility = Visibility.Visible;
        }
        private void  Simulator_Button(object sender, RoutedEventArgs e)
        {
            if(worker.IsBusy == true)
            {
                Button change = sender as Button;
                change.Content = "Simulator";
                S = true;
                DisplayUpdate();
            }
            else
            {
                worker.RunWorkerAsync();
                S = false;
                Button change = sender as Button;
                change.Content = "Stop";
                HideUpdate();
            }
        }
    }
}