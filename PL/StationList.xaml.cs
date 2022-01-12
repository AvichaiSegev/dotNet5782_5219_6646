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
    /// Interaction logic for StationList.xaml
    /// </summary>
    public partial class StationList : Window
    {
        BlApi.IBL ibl;
        public StationList(BlApi.IBL V)
        {
            ibl = V;
            InitializeComponent();
            StationsListView.ItemsSource = ibl.displayStationList();
        }
        private void DronesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.StationToList)(StationsListView.SelectedItem)).id;
            Station s = new Station(ibl, ibl.displayStation(id));
            s.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Station s = new Station(ibl);
            s.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) { StationListWindow.Close(); }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            StationListWindow.Close();
            StationList s = new StationList(ibl);
            s.Show();
        }
    }
}
