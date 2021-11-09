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
        public class CustomerToList
        {
            public int id { set; get; }
            public string name { set; get; }
            public string phone { set; get; }
            public int parcelsWasSendedAndprovided { set; get; }
            public int parcelsWasSendedButDidntProvidedYet { set; get; }
            public int parcelsGetted { set; get; }
            public int parcelOnTheWay { set; get; }
        }
    }
}
