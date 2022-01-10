using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BO
{
    public class CustomerToList
    {
        public int id { set; get; }
        public string name { set; get; }
        public string phone { set; get; }
        public int parcelsWasSendedAndprovided { set; get; }
        public int parcelsWasSendedButDidntProvidedYet { set; get; }
        public int parcelsGetted { set; get; }
        public int parcelOnTheWay { set; get; }
        public override string ToString()
        {
            return "Customer Id: " + this.id +
                    "\n     Customer name: " + this.name +
                    "\n     Customer phone: " + this.phone +
                    "\n     Parcels on the way to customer: " + this.parcelOnTheWay + 
                    "\n     Parcels getted: " + this.parcelOnTheWay + 
                    "\n     Parcels sendered but didn't provided: " + this.parcelsWasSendedButDidntProvidedYet + 
                    "\n     Parcels sendered and provided: " + this.parcelsWasSendedAndprovided;
        }
    }
}