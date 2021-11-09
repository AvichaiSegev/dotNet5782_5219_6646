using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class StationToList
        {
            public int id { set; get; }
            public string name { set; get; }
            public int numFreeChargingStands { set; get; }
            public int numBusyChargingStands { set; get; }
        }
    }
}
