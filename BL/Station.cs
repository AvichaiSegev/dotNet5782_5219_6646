using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Station
        {
            public int id { set; get; }
            public int name { set;  get; }
            public Location location { set; get; }
            public int numFreeChargingStands { set; get; }
            public List<DroneInCharging> dronesInCharging { set; get; }
            public override string ToString()
            {
                return "Station Id: " + this.id +
                        "\nStation name: " + this.name +
                        "\nStation longitude: " + this.location.longitude +
                        "\nStation lattitude: " + this.location.latitude +
                        "\nStation charge slots: " + this.numFreeChargingStands;
            }
        }
    }
}
