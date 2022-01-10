using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BO
{
    public class DroneToList
    {
        public int id { set; get; }
        public string model { set; get; }
        public WeightCategories maxWeight { set; get; }
        public double battery { set; get; }
        public DroneStatus status { set; get; }
        public Location location { set; get; }
        public int parcelNumber { set; get; }
        public override string ToString()
        {
            return "Drone Id: " + this.id +
                    "\n     Drone model: " + this.model +
                    "\n     Drone max weight: " + this.maxWeight +
                    "\n     Drone battery: " + this.battery +
                    "\n     Drone status: " + this.status +
                    "\n     Drone latitude: " + this.location.latitude +
                    "\n     Drone longitude: " + this.location.longitude +
                    "\n     Drone parcel number: " + this.parcelNumber;
        }

    }
}