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
                return "ERROR! id does not exist!\n";
            }
        }
    }
}
