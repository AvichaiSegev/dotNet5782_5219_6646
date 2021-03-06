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
using System.Runtime.CompilerServices;

namespace PL
{
    public partial class Station : Window
    {
        BlApi.IBL ibl;
        public BO.Station station { get; set; }
        bool AOU;
        public Station(BlApi.IBL V)
        {
            InitializeComponent();
            AOU = true;
            DataContext = this;
            station = new BO.Station();
            station.location = new BO.Location(0, 0);
            ibl = V;
        }
        public Station(BlApi.IBL V, BO.Station _station)
        {
            InitializeComponent();
            AOU = false;
            station = _station;
            DataContext = this;
            ibl = V;
            name.IsReadOnly = true;
            id.IsReadOnly = true;
            longitude.IsReadOnly = true;
            lattitude.IsReadOnly = true;
            AddButton.Content = "Update";
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AOU) { ibl.AddStation(station); }
            if (!AOU) { ibl.UpdateStation(station); }
            StationWindow.Close();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e){ StationWindow.Close(); }
    }
}
