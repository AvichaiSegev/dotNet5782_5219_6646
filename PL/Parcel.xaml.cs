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

    public partial class Parcel : Window
    {
        BlApi.IBL ibl;
        public BO.Parcel parcel { get; set; }
        public Parcel(BlApi.IBL V)
        {
            InitializeComponent();
            A.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            B.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            parcel = new BO.Parcel();
            parcel.Id = 96;
            ibl = V;
        }
        public Parcel(BlApi.IBL V, BO.Parcel parcel)
        {
            InitializeComponent();
            ibl = V;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e){ ParcelWindow.Close(); }
    }
}
