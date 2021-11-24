using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    namespace DO
    {
        public class IdDoesNotExistException : Exception
        {
            public int ID;
            public IdDoesNotExistException (int id)
            {
                ID = id;
            }

            public override string ToString()
            {
                return "ERROR! id "+ID+" does not exist!\n";
            }
        }
        public class LocationDoesNotExistException : Exception
        {
            public LocationDoesNotExistException()
            {
                
            }

            public override string ToString()
            {
                return "ERROR! station in this location does not exist!\n";
            }
        }

        public class IdAlreadyExistException : Exception
        {
            public int ID;
            public IdAlreadyExistException(int id)
            {
                ID = id;
            }

            public override string ToString()
            {
                return "ERROR! id " + ID + " already exist!\n";
            }
        }
    }
}
