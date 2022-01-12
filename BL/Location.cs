using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;

namespace BO
{
    public class Location
    {
        public Location(double longitude, double latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }

        public double longitude { set; get; }
        public double latitude { set; get; }
    }
}