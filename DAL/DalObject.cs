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
                Random r = new Random();
                for (int i = 0; i < 2; ++i)
                {
                    StationList.Add(new Station { Id = i, ChargeSlots = r.Next(10), Name = i, Longitude = i, Lattitude = i });
                }
                for (int i = 0; i < 5; i++)
                {
                    DroneList.Add(new Drone { Id = i, Model = "Model", Battery = r.Next(), MaxWeight = , Status =  }) ;
                }
                for (int i = 0; i < 10; i++)
                {
                    CostumerList.Add(new Costumer { Id = i, Name = "Costumer " + i, Phone = "050" + i + i + i + i + i + i + i, Longitude = i, Lattitude = i}) ;
                }
                for (int i = 0; i < 10; i++)
                {
                    ParcelList.Add(new Parcel { Id = i, SenderId = r.Next(10), TargetId = r.Next(10), DroneID = r.Next(5), Requested = , Scheduled = , PickedUp = , Delivered = , Priority = , Weight =  }) ;
                }
            }
        }
    }
}
