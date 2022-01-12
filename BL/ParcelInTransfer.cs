using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BO
{
    public class ParcelInTransfer
    {
        public int id { set; get; }
        public bool status { set; get; } // False - wait for collection, True - on the way to target
        public Priorities priority { set; get; }
        public WeightCategories weight { set; get; }
        public CustomerInParcel sender { set; get; }
        public CustomerInParcel getter { set; get; }
        public Location collection { set; get; }
        public Location target { set; get; }
        public double distance { set; get; }

    }
}