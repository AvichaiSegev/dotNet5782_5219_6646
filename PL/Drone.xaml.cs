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
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        BlApi.IBL ibl;
        public Drone(BlApi.IBL V)
        {
            InitializeComponent();
            A.Visibility = Visibility.Visible;
            B.Visibility = Visibility.Visible;
            AddButton.Visibility = Visibility.Visible;
            CloseButton.Visibility = Visibility.Visible;
            TextBlock1.Visibility = Visibility.Visible;
            TextBlock2.Visibility = Visibility.Visible;
            model.Visibility = Visibility.Visible;
            id.Visibility = Visibility.Visible;
            A.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            B.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            ibl = V;
        }
        public Drone(BlApi.IBL V, BO.Drone _drone)
        {
            InitializeComponent();
            ibl = V;
            IdText.Text = "" + _drone.id;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Drone D = new BO.Drone();
            int StationId = 0;
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
            ibl.AddDrone(D, StationId);
            DroneWindow.Close();
            DroneList d = new(ibl);
            d.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) { DroneWindow.Close(); DroneList d = new(ibl); d.Show(); }
    }
}