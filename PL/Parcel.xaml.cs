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
        bool AOU;
        public BO.Parcel parcel { get; set; }
        public Parcel(BlApi.IBL V)
        {
            InitializeComponent();
            AOU = true;
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
            AOU = false;
            parcel = _parcel;
            DataContext = this;
            A.Visibility = Visibility.Hidden;
            B.Visibility = Visibility.Hidden;
            ParcelID.IsReadOnly = true;
            SenderID.IsReadOnly = true;
            GetterID.IsReadOnly = true;
            AddButton.Content = "Update";
            ibl = V;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (AOU) { ibl.AddParcel(parcel, parcel.delivered.id, parcel.getted.id); }
            ParcelWindow.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e){ ParcelWindow.Close(); }
    }
}
