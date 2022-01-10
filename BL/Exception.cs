using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class dronesStatusIsNotApplicable : Exception
    {

    }
    public class dontHaveMuchBattery: Exception
    {

    }
    public class IdDoesNotExist: Exception
    {
        public int ID;
        public IdDoesNotExist(int id)
        {
            ID = id;
        }

        public override string ToString()
        {
            return "ERROR! id " + ID + " does not exist!\n";
        }
    }
    public class NoSuiTablePackageFound: Exception
    {

    }
    public class DroneDoesNotSuitable: Exception
    {

    }
}