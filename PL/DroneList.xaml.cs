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
    public partial class DroneList : Window
    {
        private BO.WeightCategories? WC = null;
        private BO.DroneStatus? DS = null;

        BlApi.IBL ibl;
        public DroneList(BlApi.IBL V)
        {
            ibl = V;
            InitializeComponent();
            DronesListView.ItemsSource = ibl.displayDroneList();
            A.ItemsSource = Enum.GetValues(typeof(BO.DroneStatus));
            B.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Drone d = new Drone(ibl);
            d.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e){ DroneListWindow.Close(); }
        private void Button_Click_3(object sender, RoutedEventArgs e){ DronesListView.Items.Refresh(); }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private void A_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DS = (BO.DroneStatus)A.SelectedIndex;
            DronesListView.ItemsSource = ibl.displayDroneListFiltered(WC, DS);
        }
        private void B_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WC = (BO.WeightCategories)B.SelectedIndex;
            DronesListView.ItemsSource = ibl.displayDroneListFiltered(WC, DS);
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            Drone d = new Drone(ibl, ibl.displayDrone(((BO.DroneToList)DronesListView.SelectedItem).id));
            d.Show();
        }
    }
}
