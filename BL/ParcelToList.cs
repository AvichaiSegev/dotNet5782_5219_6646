using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace BO
{
    public class ParcelToList
    {
        public int parcelId { set; get; }
        public string deliveredCustomerName { set; get; }
        public string gettedCustomerName { set; get; }
        public WeightCategories weight { set; get; }
        public Priorities priority { set; get; }
        public ParcelStatus parcelStatus { set; get; }
        public override string ToString()
        {
            return "parcel Id: " + this.parcelId +
                    "\n     Parcel delivered name: " + this.deliveredCustomerName +
                    "\n     Parcel getted name: " + this.gettedCustomerName +
                    "\n     Parcel priority: " + this.priority +
                    "\n     Parcel weight: " + this.weight +
                    "\n     Parcel status: " + this.parcelStatus;
        }
    }
}