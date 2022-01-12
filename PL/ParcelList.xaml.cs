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
    public partial class ParcelList : Window
    {
        private BO.WeightCategories? W = null;
        private BO.Priorities? P = null;
        BlApi.IBL ibl;
        public ParcelList(BlApi.IBL V)
        {
            ibl = V;
            InitializeComponent();
            parcelsListView.ItemsSource = ibl.displayParcelList();
            A.ItemsSource = Enum.GetValues(typeof(BO.Priorities));
            B.ItemsSource = Enum.GetValues(typeof(BO.WeightCategories));
        }
        private void ParcelsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.ParcelToList)(parcelsListView.SelectedItem)).parcelId;
            Parcel s = new Parcel(ibl, ibl.displayParcel(id));
            s.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Parcel p = new Parcel(ibl);
            p.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) { parcelListWindow.Close(); }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            parcelListWindow.Close();
            ParcelList p = new ParcelList(ibl);
            p.Show();
        }

        private void A_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            P = (BO.Priorities)A.SelectedIndex;
            parcelsListView.ItemsSource = ibl.displayParcelListFiltered(W, P, null);
        }

        private void B_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            W = (BO.WeightCategories)B.SelectedIndex;
            parcelsListView.ItemsSource = ibl.displayParcelListFiltered(W, P, null);
        }

        private void DoButton_Click(object sender, RoutedEventArgs e)
        {
            parcelsListView.ItemsSource = ibl.displayParcelListFiltered(FirstDate.SelectedDate, SecondDate.SelectedDate);
        }
    }
}
