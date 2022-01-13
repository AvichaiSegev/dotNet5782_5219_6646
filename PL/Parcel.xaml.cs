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
    public partial class Parcel : Window
    {
        BlApi.IBL ibl;
        public BO.Parcel parcel { get; set; }
        public Parcel(BlApi.IBL V)
        {
            InitializeComponent();
            DataContext = this;
            A.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            B.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
            parcel = new BO.Parcel();
            ibl = V;
            parcel.delivered = new BO.CustomerInParcel();
            parcel.getted = new BO.CustomerInParcel();
        }
        public Parcel(BlApi.IBL V, BO.Parcel _parcel)
        {
            InitializeComponent();
            parcel = _parcel;
            DataContext = this;
            ibl = V;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ibl.AddParcel(parcel, parcel.delivered.id, parcel.getted.id);
            ParcelWindow.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e){ ParcelWindow.Close(); }
    }
}
