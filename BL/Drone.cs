using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;

namespace BO    
{
    public class Drone
    {
        public int id { set; get; }
        public string model { set; get; }
        public WeightCategories maxWeight { set; get; }
        public double battery { set; get; }
        public DroneStatus status { set; get; }
        public ParcelInTransfer parcel { set; get; }
        public Location location { set; get; }
        public override string ToString()
        {
            if (parcel.id != int.MinValue)
            {
                return "Drone Id: " + this.id +
                        "\nDrone model: " + this.model +
                        "\nDrone maxWeight: " + this.maxWeight +
                        "\nDrone battery: " + this.battery +
                        "\nDrone status: " + this.status +
                        "\nDrone longitude: " + this.location.longitude +
                        "\nDrone lattitude: " + this.location.latitude +
                        "\nDrone parcel id: " + this.parcel.id;
            }
            return "Drone Id: " + this.id +
                        "\nDrone model: " + this.model +
                        "\nDrone maxWeight: " + this.maxWeight +
                        "\nDrone battery: " + this.battery +
                        "\nDrone status: " + this.status +
                        "\nDrone longitude: " + this.location.longitude +
                        "\nDrone lattitude: " + this.location.latitude;
        }
    }
}