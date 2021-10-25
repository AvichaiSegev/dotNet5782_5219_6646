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
            static internal List<DroneCharge> DroneChargeList = new List<DroneCharge>();
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
                    DroneList.Add(new Drone { Id = i, Model = "Model", Battery = r.Next(), MaxWeight = WeightCategories.light, Status = DroneStatuses.available }) ;
                }
                for (int i = 0; i < 10; i++)
                {
                    CostumerList.Add(new Costumer { Id = i, Name = "Costumer " + i, Phone = "0" + i + i + i + i + i + i + i + i + i, Longitude = i, Lattitude = i}) ;
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
            static public void AddParcel(int parcelId, int senderId, int targetId, WeightCategories parcelWeight, Priorities priority) { DataSource.ParcelList.Add(new Parcel { Id = parcelId, SenderId = senderId, TargetId = targetId, Weight = parcelWeight, Priority = priority, Requested = DateTime.Now }); }
            static public void schedule(int parcelId)
            {
                Parcel theParcel = new Parcel();
                Drone theDrone = new Drone();
                foreach (Parcel item in DataSource.ParcelList){ if (parcelId == item.Id) { theParcel = item; } }
                foreach (Drone item in DataSource.DroneList)
                {
                    if (item.MaxWeight >= theParcel.Weight && item.Status == DroneStatuses.available)
                    {
                        theDrone = item;
                        int parcelIndex = DataSource.ParcelList.IndexOf(theParcel);
                        int droneIndex = DataSource.DroneList.IndexOf(theDrone);
                        theDrone.Status = DroneStatuses.shipment;
                        theParcel.Scheduled = DateTime.Now;
                        theParcel.DroneId = item.Id;
                        DataSource.ParcelList[parcelIndex] = theParcel;
                        DataSource.DroneList[droneIndex] = theDrone;
                        break;
                    }
                }
            }
            static public void pickUp(int parcelId)
            {
                Parcel theParcel = new Parcel();
                foreach (Parcel item in DataSource.ParcelList) { if (parcelId == item.Id) { theParcel = item; } }
                int parcelIndex = DataSource.ParcelList.IndexOf(theParcel);
                theParcel.PickedUp = DateTime.Now;
                DataSource.ParcelList[parcelIndex] = theParcel;
            }
            static public void deliver(int parcelId)
            {
                Parcel theParcel = new Parcel();
                Drone theDrone = new Drone();
                foreach (Parcel item in DataSource.ParcelList) { if (parcelId == item.Id) { theParcel = item; } }
                foreach (Drone item in DataSource.DroneList) { if (theParcel.DroneId == item.Id) { theDrone = item; } }
                int parcelIndex = DataSource.ParcelList.IndexOf(theParcel);
                int droneIndex = DataSource.DroneList.IndexOf(theDrone);
                theParcel.Delivered = DateTime.Now;
                theDrone.Status = DroneStatuses.available;
                DataSource.ParcelList[parcelIndex] = theParcel;
                DataSource.DroneList[droneIndex] = theDrone;
            }
            static public void charge(int droneId, int stationId)
            {
                Station theStation = new Station();
                Drone theDrone = new Drone();
                foreach (Station item in DataSource.StationList) { if (item.Id == stationId) { theStation = item; } }
                foreach (Drone item in DataSource.DroneList) { if(item.Id == droneId){ theDrone = item; } }
                int stationIndex = DataSource.StationList.IndexOf(theStation);
                int droneIndex = DataSource.DroneList.IndexOf(theDrone);
                theStation.ChargeSlots-=1;
                theDrone.Status = DroneStatuses.maintenance;
                DataSource.StationList[stationIndex] = theStation;
                DataSource.DroneList[droneIndex] = theDrone;
                DataSource.DroneChargeList.Add(new DroneCharge { Droneld = droneId, Stationld = stationId });
            }
            static public void unCharge(int droneId)
            {
                Station theStation = new Station();
                Drone theDrone = new Drone();
                foreach (Drone item in DataSource.DroneList) { if (item.Id == droneId) { theDrone = item; } }
                foreach (Station item1 in DataSource.StationList)
                {
                    foreach (DroneCharge item2 in DataSource.DroneChargeList)
                    {
                        if (item1.Id == item2.Stationld) { theStation = item1; DataSource.DroneChargeList.Remove(item2); }
                        break;
                    }
                }
                int stationIndex = DataSource.StationList.IndexOf(theStation);
                int droneIndex = DataSource.DroneList.IndexOf(theDrone);
                theStation.ChargeSlots += 1;
                theDrone.Status = DroneStatuses.available;
                DataSource.StationList[stationIndex] = theStation;
                DataSource.DroneList[droneIndex] = theDrone;
            }
        }
    }
}