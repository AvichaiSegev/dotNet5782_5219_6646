using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
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
        }
    }
}
