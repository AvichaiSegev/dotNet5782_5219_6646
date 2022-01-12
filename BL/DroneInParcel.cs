using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;

namespace BO
{
    public class DroneInParcel
    {
        public int id { set; get; }
        public double battery { set; get; }
        public Location location { set; get; }
    }
}