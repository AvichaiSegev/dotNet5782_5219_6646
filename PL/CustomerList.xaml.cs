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
    public partial class CustomerList : Window
    {
        BlApi.IBL ibl;
        public CustomerList(BlApi.IBL V)
        {
            ibl = V;
            InitializeComponent();
            CustomersListView.ItemsSource = ibl.displayCustomerList();
        }
        private void CustomersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.CustomerToList)(CustomersListView.SelectedItem)).id;
            Customer c = new Customer(ibl, ibl.displayCustomer(id));
            c.Show();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Customer c = new Customer(ibl);
            c.Show();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e){ CustomerListWindow.Close(); }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            CustomerListWindow.Close();
            CustomerList c = new CustomerList(ibl);
            c.Show();
        }
    }
}
