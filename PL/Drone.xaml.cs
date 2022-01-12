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

namespace PL
{
    public partial class Drone : Window
    {
        BlApi.IBL ibl;
        BO.Drone drone;
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
            drone.status = BO.DroneStatus.free;
            A.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            B.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            ibl = V;
        }
        public Drone(BlApi.IBL V, BO.Drone _drone)
        {
            InitializeComponent();
            DataContext = this;
            ibl = V;
            IdText.Text = "" + _drone.id;
            ModelText.Text = _drone.model;
            LongitudeText2.Text = "" + _drone.location.longitude;
            LattitudeText2.Text = "" + _drone.location.latitude;
            WeightText2.Text = "" + _drone.maxWeight;
            BatteryText2.Text = "" + _drone.battery;
            StatusText2.Text = "" + _drone.status;
            drone = _drone;
            if (drone.status == BO.DroneStatus.free)
            {
                ChangeButton1.Content = "Assign to parcel";
            }
            if (drone.status == BO.DroneStatus.delivery) { ChangeButton2.Visibility = Visibility.Hidden; ChargingTime.Visibility = Visibility.Hidden; }
            if (drone.status == BO.DroneStatus.free) { ChangeButton2.Content = "Send to charge"; ChargingTime.Visibility = Visibility.Hidden; }
            if (drone.status == BO.DroneStatus.matance) { ChangeButton2.Content = "Release from charge"; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Drone D = new BO.Drone();
            D.model = model.Text;
            if(id.Text != "")D.id = Convert.ToInt32(id.Text);
            else { D.id = 0; }
            switch (A.SelectedIndex)
            {
                case 0:
                    D.status = (BO.DroneStatus)0;
                    break;
                case 1:
                    D.status = (BO.DroneStatus)1;
                    break;
                case 2:
                    D.status = (BO.DroneStatus)2;
                    break;
                default:
                    break;
            }
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
            DroneList d = new(ibl);
            d.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) { DroneWindow.Close(); DroneList d = new(ibl); d.Show(); }
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
                ibl.assignParcelToDrone(drone.id);                
            }
            if(drone.status == BO.DroneStatus.delivery) { }
        }

        private void ChangeButton2_Click(object sender, RoutedEventArgs e)
        {
            double D;
            if (!double.TryParse(ChargingTime.Text, out D)) { D = 0; }
            if (drone.status == BO.DroneStatus.free) { ibl.sendDroneToCharging(drone.id); }
            else if (drone.status == BO.DroneStatus.matance) { ibl.releaseDroneFromCharging(drone.id, D); }

        }
    }
}