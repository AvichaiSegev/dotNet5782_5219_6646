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
        public DroneList(IBL.IBL V)
        {
            ibl = V;
            InitializeComponent();
            DronesListView.ItemsSource = ibl.displayDroneList();
            A.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatus));
            B.ItemsSource = Enum.GetValues(typeof(IBL.BO.Priorities));
        }
        IBL.IBL ibl;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Drone d = new Drone(new BL.BL());
            d.Show();
        }
    }
}
