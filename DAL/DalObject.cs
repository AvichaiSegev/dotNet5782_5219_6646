using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DAL
{
    namespace DalObject
    {
        class DataSource
        {
            static internal List<Drone> DroneList = new List<Drone>();
            static internal List<Station> StationList = new List<Station>();
            static internal List<Costumer> CostumerList = new List<Costumer>();
            static internal List<Parcel> ParcelList = new List<Parcel>();
            internal class Config
            {
                static internal int IDParcel;
            }
            static internal void Initialize()
            {
                for (int i = 0; i < 2; ++i){ StationList.Add(new Station { Id = 0, ChargeSlots = 0, Name = 2, Longitude = 2, Lattitude = 2 }); }
                //for (int i = 0; i < length; i++)
                {

                }
            }
        }
    }
}
