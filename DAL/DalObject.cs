using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;
using DAL.DalObject;

namespace DAL
{
    namespace DalObject
    {
        public class DataSource
        {

            //Create te lists:
            internal List<Drone> DroneList = new List<Drone>();
            internal List<Station> StationList = new List<Station>();
            internal List<customer> customerList = new List<customer>();
            internal List<Parcel> ParcelList = new List<Parcel>();
            internal List<DroneCharge> DroneChargeList = new List<DroneCharge>();
            public DataSource() => Initialize();
            
            //Create function for initialize the lists:
            internal void Initialize()
            {
                int NumberForStations = 100;
                Random r = new Random();
                for (int i = 0; i < 2; i++)
                {
                    StationList.Add(new Station { Id = i, ChargeSlots = r.Next(10), Name = i, Longitude = i, Lattitude = i });
                }
                for (int i = 0; i < 5; i++)
                {
                    DroneList.Add(new Drone { Id = i, Model = "Model", MaxWeight = WeightCategories.light}) ;
                }
                for (int i = 0; i < 10; i++)
                {
                    customerList.Add(new customer { Id = i, Name = "customer " + i, Phone = "0" + i + i + i + i + i + i + i + i + i, Longitude = i, Lattitude = i}) ;
                }
                for (int i = 0; i < 10; i++)
                {
                    ParcelList.Add(new Parcel { Id = i, SenderId = r.Next(10), TargetId = r.Next(10), DroneId = r.Next(5), Priority = Priorities.regular, Weight = WeightCategories.light }) ;
                }
            }
            internal class Config
            {
                internal double electricityUseForClearing, electricityUseForLight, electricityUseForMedium, electricityUseForLiver, ChargingRate;

            }
        }
        public class DalObject : IDAL.IDal
        {
            DataSource data = new DataSource();
            
            //Run "initialize" in the constructor: 

            //Create function for display objects:
            public Station displayStation(int Id)
            {
                foreach (Station item in data.StationList){ if (item.Id == Id) { return item; } }
                return new Station { Id = -1, Name = -1, Longitude = -1, Lattitude = -1, ChargeSlots = -1 };
            }
            public Drone displayDrone(int Id)
            {
                foreach (Drone item in data.DroneList){ if (item.Id == Id) { return item; } }
                return new Drone { Id = -1, Model = "None", MaxWeight = WeightCategories.light};
            }
            public customer displaycustomer(int Id)
            {
                foreach (customer item in data.customerList){ if (item.Id == Id) { return item; } }
                return new customer { Id = -1, Name = "None", Phone = "0", Longitude = 0, Lattitude = 0 };
            }
            public Parcel displayParcel(int Id)
            {
                foreach (Parcel item in data.ParcelList){ if (item.Id == Id) { return item; } }
                return new Parcel { Id = -1, SenderId = -1, TargetId = -1, Weight = WeightCategories.light, Priority = Priorities.regular, Requested = DateTime.Now, Scheduled = DateTime.Now, PickedUp = DateTime.Now, Delivered = DateTime.Now, DroneId = -1 };
            }

            //Create function for create list of the available stations:
            public List<int> IdListForStations()
            {
                List<int> list = new List<int>();
                foreach (Station item in data.StationList) { if (item.ChargeSlots > 0) { list.Add(item.Id); } }
                return list;
            }

            //Create functions for display list of object:
            public IEnumerable<Station> displayStationList() { return data.StationList; }
            public IEnumerable<Drone> displayDroneList() { return data.DroneList; }
            public IEnumerable<customer> displaycustomerList() { return data.customerList; }
            public IEnumerable<Parcel> displayParcelList() { return data.ParcelList; }
            
            //Create function for add objects to the list:
            public void AddStation(int stationId, int stationName, double stationLongitude, double stationLattitude, int chargeSlots) { data.StationList.Add(new Station { Id = stationId, Name = stationName, Longitude = stationLongitude, Lattitude = stationLattitude, ChargeSlots = chargeSlots }); }
            public void AddDrone(int droneId, string droneModel, WeightCategories droneMaxWeight) { data.DroneList.Add(new Drone { Id = droneId, Model = droneModel, MaxWeight = droneMaxWeight}); }
            public void Addcustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude) { data.customerList.Add(new customer { Id = customerId, Name = customerName, Phone = customerPhone, Longitude = customerLongitude, Lattitude = customerLattitude }); }
            public void AddParcel(int parcelId, int senderId, int targetId, WeightCategories parcelWeight, Priorities priority) { data.ParcelList.Add(new Parcel { Id = parcelId, SenderId = senderId, TargetId = targetId, Weight = parcelWeight, Priority = priority, Requested = DateTime.Now }); }

            //Create function for update objects in the list:
            public void UpdateStation(int stationId, int stationName, double stationLongitude, double stationLattitude, int chargeSlots)
            {
                int i = 0;
                while(data.StationList[i].Id != stationId){
                    i++;
                }
                Station station = data.StationList[i];
                station.Name = stationName;
                station.Longitude = stationLongitude;
                station.Lattitude = stationLattitude;
                station.ChargeSlots = chargeSlots;
                data.StationList[i] = station;
            }

            public void UpdateDrone(int droneId, string droneModel, WeightCategories droneMaxWeight)
            {
                int i = 0;
                while (data.DroneList[i].Id != droneId)
                {
                    i++;
                }
                Drone drone = data.DroneList[i];
                drone.Model = droneModel;
                drone.MaxWeight = droneMaxWeight;
                data.DroneList[i] = drone;
            }

            public void Updatecustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude)
            {
                int i = 0;
                while (data.customerList[i].Id != customerId)
                {
                    i++;
                }
                customer customer = data.customerList[i];
                customer.Name = customerName;
                customer.Longitude = customerLongitude;
                customer.Lattitude = customerLattitude;
                customer.Phone = customerPhone;
                data.customerList[i] = customer;
            }

            public void UpdateParcel(int parcelId, int senderId, int targetId, WeightCategories parcelWeight, Priorities priority)
            {

                checkId(id);


                int i = 0;
                while (data.ParcelList[i].Id != parcelId)
                {
                    i++;
                }
                Parcel parcel = data.ParcelList[i];
                parcel.SenderId = senderId;
                parcel.TargetId = targetId;
                parcel.Weight = parcelWeight;
                parcel.Priority = priority;
                data.ParcelList[i] = parcel;
            }

            public double[] electricityUse()
            {

                return new double[]{ 1, 10, 20, 30, 50 }; 
            }

        }
    }
}