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
    public partial class Customer : Window
    {
        BlApi.IBL ibl;
        public BO.Customer customer { get; set; }
        bool AOU;
        public Customer(BlApi.IBL V)
        {
            InitializeComponent();
            AOU = true;
            DataContext = this;
            customer = new BO.Customer();
            customer.location = new BO.Location(0, 0);
            ibl = V;
            customer.name = "-";
            customer.phone = "-";
        }
        public Customer(BlApi.IBL V, BO.Customer _customer)
        {
            InitializeComponent();
            AOU = false;
            customer = _customer;
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
            if (AOU) { ibl.Addcustomer(customer); }
            if (!AOU) { ibl.Updatecustomer(customer.id, customer.name, customer.phone, customer.location.longitude, customer.location.latitude); }
            CustomerWindow.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e){ CustomerWindow.Close(); }
    }
}