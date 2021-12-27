using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BO
{
    public class Parcel
    {

        public int Id { set; get; }
        public CustomerInParcel delivered { set; get; }
        public CustomerInParcel getted { set; get; }
        public WeightCategories weight { set; get; }
        public Priorities priority { set; get; }
        public DroneInParcel droneInParcel { set; get; }
        public DateTime? definedParcelTime { set; get; }
        public DateTime? assignedParcelTime { set; get; }
        public DateTime? collectedParcelTime { set; get; }
        public DateTime? providedParcelTime { set; get; }
        public override string ToString()
        {
            if (assignedParcelTime == null)
            {
                return "Parcel Id: " + this.Id +
                        "\nParcel weight: " + this.weight +
                        "\nParcel sender name: " + this.delivered.name +
                        "\nParcel getted name: " + this.getted.name +
                        "\nParcel priority: " + this.priority;
            }
            else if(collectedParcelTime == null)
            {
                return "Parcel Id: " + this.Id +
                        "\nParcel weight: " + this.weight +
                        "\nParcel sender name: " + this.delivered.name +
                        "\nParcel getted name: " + this.getted.name +
                        "\nParcel priority: " + this.priority +
                        "\nParcel drone id: " + this.droneInParcel.id + 
                        "\nparcel assigned time: " + this.assignedParcelTime;
            }
            else if (providedParcelTime == null)
            {
                return "Parcel Id: " + this.Id +
                        "\nParcel weight: " + this.weight +
                        "\nParcel sender name: " + this.delivered.name +
                        "\nParcel getted name: " + this.getted.name +
                        "\nParcel priority: " + this.priority +
                        "\nParcel drone id: " + this.droneInParcel.id +
                        "\nparcel assigned time: " + this.assignedParcelTime + 
                        "\nparcel collected time: " + this.collectedParcelTime;
            }
            else if (providedParcelTime != null)
            {
                return "Parcel Id: " + this.Id +
                        "\nParcel weight: " + this.weight +
                        "\nParcel sender name: " + this.delivered.name +
                        "\nParcel getted name: " + this.getted.name +
                        "\nParcel priority: " + this.priority +
                        "\nParcel drone id: " + this.droneInParcel.id +
                        "\nparcel assigned time: " + this.assignedParcelTime +
                        "\nparcel collected time: " + this.collectedParcelTime +
                        "\nparcel provided time: " + this.providedParcelTime;
            }
            return "Parcel Id: " + this.Id +
                        "\nParcel weight: " + this.weight +
                        "\nParcel sender name: " + this.delivered.name +
                        "\nParcel getted name: " + this.getted.name +
                        "\nParcel priority: " + this.priority;
        }
    }
}