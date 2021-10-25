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
        public static class DataSource
        {
            static internal List<Drone> DroneList = new List<Drone>();
            static internal List<Station> StationList = new List<Station>();
            static internal List<Costumer> CostumerList = new List<Costumer>();
            static internal List<Parcel> ParcelList = new List<Parcel>();
            static DataSource() => Initialize();
            static internal void Initialize()
            {
                Random r = new Random();
                for (int i = 0; i < 2; i++)
                {
                    StationList.Add(new Station { Id = i, ChargeSlots = r.Next(10), Name = i, Longitude = i, Lattitude = i });
                }
                for (int i = 0; i < 5; i++)
                {
                    DroneList.Add(new Drone { Id = i, Model = "Model", Battery = r.Next(), MaxWeight = WeightCategories.light, Status = DroneStatuses.shipment }) ;
                }
                for (int i = 0; i < 10; i++)
                {
                    CostumerList.Add(new Costumer { Id = i, Name = "Costumer " + i, Phone = "05" + i + i + i + i + i + i + i + i, Longitude = i, Lattitude = i}) ;
                }
                for (int i = 0; i < 10; i++)
                {
                    ParcelList.Add(new Parcel { Id = i, SenderId = r.Next(10), TargetId = r.Next(10), DroneId = r.Next(5), Requested = DateTime.Now, Scheduled = DateTime.Now, PickedUp = DateTime.Now, Delivered = DateTime.Now, Priority = Priorities.regular, Weight = WeightCategories.light }) ;
                }
            }
            static internal class Config
            {
                static internal int IDParcel;
            }
        }

        public class DalObject
        {
            public DalObject(){ DataSource.Initialize(); }

            static public Station displayStation(int Id)
            {
                foreach (Station item in DataSource.StationList){ if (item.Id == Id) { return item; } }
                return new Station { Id = -1, Name = -1, Longitude = -1, Lattitude = -1, ChargeSlots = -1 };
            }
            static public Drone displayDrone(int Id)
            {
                foreach (Drone item in DataSource.DroneList){ if (item.Id == Id) { return item; } }
                return new Drone { Id = -1, Model = "None", MaxWeight = WeightCategories.light, Status = DroneStatuses.available, Battery = -1 };
            }
            static public Costumer displayCostumer(int Id)
            {
                foreach (Costumer item in DataSource.CostumerList){ if (item.Id == Id) { return item; } }
                return new Costumer { Id = -1, Name = "None", Phone = "0", Longitude = 0, Lattitude = 0 };
            }
            static public Parcel displayParcel(int Id)
            {
                foreach (Parcel item in DataSource.ParcelList){ if (item.Id == Id) { return item; } }
                return new Parcel { Id = -1, SenderId = -1, TargetId = -1, Weight = WeightCategories.light, Priority = Priorities.regular, Requested = DateTime.Now, Scheduled = DateTime.Now, PickedUp = DateTime.Now, Delivered = DateTime.Now, DroneId = -1 };
            }
            static public List<Station> displayStationList() { return DataSource.StationList; }
            static public List<Drone> displayDroneList() { return DataSource.DroneList; }
            static public List<Costumer> displayCostumerList() { return DataSource.CostumerList; }
            static public List<Parcel> displayParcelList() { return DataSource.ParcelList; }
            static public void AddStation(int stationId, int stationName, double stationLongitude, double stationLattitude, int chargeSlots) { DataSource.StationList.Add(new Station { Id = stationId, Name = stationName, Longitude = stationLongitude, Lattitude = stationLattitude, ChargeSlots = chargeSlots }); }
            static public void AddDrone(int droneId, string droneModel, WeightCategories droneMaxWeight, DroneStatuses droneStatus, double droneBattery) { DataSource.DroneList.Add(new Drone { Id = droneId, Model = droneModel, MaxWeight = droneMaxWeight, Status = droneStatus, Battery = droneBattery }); }
            static public void AddCostumer(int costumerId, string costumerName, string costumerPhone, double costumerLongitude, double costumerLattitude) { DataSource.CostumerList.Add(new Costumer { Id = costumerId, Name = costumerName, Phone = costumerPhone, Longitude = costumerLongitude, Lattitude = costumerLattitude }); }
        }
    }
}