using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BO
{
    public class Customer
    {
        public int id { set; get; }
        public string name { set; get; }
        public string phone { set; get; }
        public Location location { set; get; }
        public List<ParcelAtTheCustomer> fromTheCustomer { set; get; }
        public List<ParcelAtTheCustomer> forTheCustomer { set; get; }
        public override string ToString()
        {
            return "Customer Id: " + this.id +
                    "\nCustomer name: " + this.name +
                    "\nCustomer phone: " + this.phone +
                    "\nCustomer latitude: " + this.location.latitude +
                    "\nCustomer longitude: " + this.location.longitude;
        }
    }
}