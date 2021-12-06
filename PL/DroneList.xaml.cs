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
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneList : Window
    {
        IBL.IBL ibl;
        public DroneList(IBL.IBL V)
        {
            ibl = V;
            InitializeComponent();
            DronesListView.ItemsSource = ibl.displayDroneList("All", 0);
            A.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatus));
            B.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Drone d = new Drone(ibl);
            d.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e){ DroneListWindow.Close(); }

        private void A_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {            
            Drone d = new Drone(ibl, (IBL.BO.DroneToList)DronesListView.SelectedItem);
            d.Show();
        }
    }
}
