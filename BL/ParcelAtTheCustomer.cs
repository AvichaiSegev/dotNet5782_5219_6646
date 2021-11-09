using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class ParcelAtTheCustomer
        {
            public int id { set; get; }
            public WeightCategories Weight { set; get; }
            public Priorities priority { set; get; }
            public ParcelStatus status { set; get; }
            public CustomerInParcel customer { set; get; }
        }
    }
}
