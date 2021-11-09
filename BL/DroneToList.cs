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
        public class DroneToList
        {
            public int id { set; get; }
            public string model { set; get; }
            public WeightCategories maxWeight { set; get; }
            public int battery { set; get; }
            public DroneStatus status { set; get; }
            public Location location { set; get; }
            public int parcelNumber { set; get; }

        }
    }
}
