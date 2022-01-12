using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BO
{
    public class StationToList
    {
        public int id { set; get; }
        public string name { set; get; }
        public int numFreeChargingStands { set; get; }
        public int numBusyChargingStands { set; get; }
        public override string ToString()
        {
            return "Station Id: " + this.id +
                    "\n     Station name: " + this.name +
                    "\n     Station busy charge slots: " + numBusyChargingStands + 
                    "\n     Station free charge slots: " + this.numFreeChargingStands;
        }
    }
}