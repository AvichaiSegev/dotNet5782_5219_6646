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
            internal List<Customer> customerList = new List<Customer>();
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
                    customerList.Add(new Customer { Id = i, Name = "customer " + i, Phone = "0" + i + i + i + i + i + i + i + i + i, Longitude = i, Lattitude = i}) ;
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
            public Customer displayCustomer(int Id)
            {
                foreach (Customer item in data.customerList){ if (item.Id == Id) { return item; } }
                return new Customer { Id = -1, Name = "None", Phone = "0", Longitude = 0, Lattitude = 0 };
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
            public IEnumerable<Customer> displayCustomerList() { return data.customerList; }
            public IEnumerable<Parcel> displayParcelList() { return data.ParcelList; }
            
            //Create function for add objects to the list:
            public void AddStation(Station station) { data.StationList.Add(station); }
            public void AddDrone(Drone drone) { data.DroneList.Add(drone); }
            public void AddCustomer(Customer customer) { data.customerList.Add(customer); }
            public void AddParcel(Parcel parcel) { data.ParcelList.Add(parcel); }

            //Create function for update objects in the list:
            public void UpdateStation(Station station)
            {
                int i = 0;
                while(data.StationList[i].Id != station.Id){
                    i++;
                }
                data.StationList[i] = station;
            }

            public void UpdateDrone(Drone drone)
            {
                int i = 0;
                while (data.DroneList[i].Id != drone.Id){
                    i++;
                }
                data.DroneList[i] = drone;
            }

            public void UpdateCustomer(Customer customer)
            {
                int i = 0;
                while (data.customerList[i].Id != customer.Id)
                {
                    i++;
                }
                data.customerList[i] = customer;
            }

            public void UpdateParcel(Parcel parcel)
            {

                int i = 0;
                while (data.ParcelList[i].Id != parcel.Id)
                {
                    i++;
                }
                data.ParcelList[i] = parcel;
            }

            public double[] electricityUse()
            {

                return new double[]{ 1, 10, 20, 30, 50 }; 
            }

        }
    }
}