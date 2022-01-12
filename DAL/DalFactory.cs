using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DalApi
{
    public static class DalFactory
    {
        public static IDal GetDal()
        {
            return DAL.DalXml.DalXml.Instace;
        }

    }
}
