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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum State { drones, stations, parcels, customers }
        public MainWindow()
        {
            InitializeComponent();
            state.ItemsSource = Enum.GetValues(typeof(State));
        }
        BlApi.IBL ibl;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ibl = BlApi.BlFactory.GetBl();
            DroneList d = new DroneList(ibl);
            StationList s = new StationList(ibl);
            CustomerList c = new CustomerList(ibl);
            ParcelList p = new ParcelList(ibl);
            if (state.SelectedIndex == 0) { d.Show(); }
            if (state.SelectedIndex == 1) { s.Show(); }
            if (state.SelectedIndex == 2) { p.Show(); }
            if (state.SelectedIndex == 3) { c.Show(); }
        }
    }
}