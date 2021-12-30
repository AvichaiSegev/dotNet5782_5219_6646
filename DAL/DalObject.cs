using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
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
            internal List<Customer> CustomerList = new List<Customer>();
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
                    StationList.Add(new Station { Id = i, freeChargeSlots = r.Next(10), Name = i, Longitude = i, Lattitude = i });
                }
                for (int i = 0; i < 5; i++)
                {
                    DroneList.Add(new Drone { Id = i, Model = "Model", MaxWeight = WeightCategories.light}) ;
                }
                for (int i = 0; i < 10; i++)
                {
                    CustomerList.Add(new Customer { Id = i, Name = "customer " + i, Phone = "0" + i + i + i + i + i + i + i + i + i, Longitude = i, Lattitude = i}) ;
                }
                for (int i = 0; i < 10; i++)
                {
                    ParcelList.Add(new Parcel { Id = i, SenderId = r.Next(10), TargetId = r.Next(10), DroneId = r.Next(5), Priority = Priorities.regular, Weight = WeightCategories.light, Assigned = DateTime.Now }) ;
                }
            }
            internal class Config
            {
                internal double electricityUseForClearing, electricityUseForLight, electricityUseForMedium, electricityUseForLiver, ChargingRate;

            }
        }
        public class DalObject : DalApi.IDal
        {
            DataSource data;
            public DalObject()
            {
                data = new DataSource();
            }

            static DalObject() { }
            private static DalObject instace;
            static readonly object lockname = new object();
            public static DalObject Instace
            {
                get
                {
                    if (instace == null)
                    {
                        lock (lockname)
                        {
                            if(instace == null)
                            {
                                instace = new DalObject();
                            }
                        }
                    }
                    return instace;
                }
            }
            //Run "initialize" in the constructor: 

            //Create function for display objects:
            public Station displayStation(int Id)
            {
                if (!data.StationList.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Station item in data.StationList){ if (item.Id == Id) { return item; } }
                return new Station { Id = -1, Name = -1, Longitude = -1, Lattitude = -1, freeChargeSlots = -1 };
            }
            public Drone displayDrone(int Id)
            {
                if (!data.DroneList.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Drone item in data.DroneList){ if (item.Id == Id) { return item; } }
                return new Drone { Id = -1, Model = "None", MaxWeight = WeightCategories.light};
            }
            public Customer displayCustomer(int Id)
            {
                if (!data.CustomerList.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Customer item in data.CustomerList){ if (item.Id == Id) { return item; } }
                return new Customer { Id = -1, Name = "None", Phone = "0", Longitude = 0, Lattitude = 0 };
            }
            public Parcel displayParcel(int Id)
            {
                if (!data.ParcelList.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Parcel item in data.ParcelList){ if (item.Id == Id) { return item; } }
                return new Parcel { Id = -1, SenderId = -1, TargetId = -1, Weight = WeightCategories.light, Priority = Priorities.regular, Defined = DateTime.Now, Assigned = DateTime.Now, Collected = DateTime.Now, Provided = DateTime.Now, DroneId = -1 };
            }

            public Station displayStationByLocation(double latitude, double longitude)
            {
                if (!data.StationList.Any(x => x.Lattitude == latitude && x.Longitude == longitude))
                {
                    throw new LocationDoesNotExistException();
                }
                foreach (Station item in data.StationList) { if (item.Longitude == longitude && item.Lattitude == latitude) { return item; } }
                return new Station { Id = -1, Name = -1, Longitude = -1, Lattitude = -1, freeChargeSlots = -1 };
            }
            //Create function for create list of the available stations:
            public List<int> IdListForStations()
            {
                List<int> list = new List<int>();
                foreach (Station item in data.StationList) { if (item.freeChargeSlots > 0) { list.Add(item.Id); } }
                return list;
            }

            //Create functions for display list of object:
            public IEnumerable<Station> displayStationList() { return data.StationList; }
            public IEnumerable<Drone> displayDroneList() { return data.DroneList; }
            public IEnumerable<Customer> displayCustomerList() { return data.CustomerList; }
            public IEnumerable<Parcel> displayParcelList() { return data.ParcelList; }
            
            //Create function for add objects to the list:
            public void AddStation(Station station)
            {
                if (data.StationList.Any(x => x.Id == station.Id))
                {
                    throw new IdAlreadyExistException(station.Id);
                }
                data.StationList.Add(station); }
            public void AddDrone(Drone drone)
            {
                if (data.DroneList.Any(x => x.Id == drone.Id))
                {
                    throw new IdAlreadyExistException(drone.Id);
                }
                data.DroneList.Add(drone); }
            public void AddCustomer(Customer customer)
            {
                if (data.CustomerList.Any(x => x.Id == customer.Id))
                {
                    throw new IdAlreadyExistException(customer.Id);
                }
                data.CustomerList.Add(customer); }
            //Function for add parcel:
            public void AddParcel(Parcel parcel) {
                if (data.ParcelList.Any(x => x.Id == parcel.Id))
                {
                    throw new IdAlreadyExistException(parcel.Id);
                }
                data.ParcelList.Add(parcel); }

            //Create function for update objects in the list:
            public void UpdateStation(Station station)
            {
                if (!data.StationList.Any(x => x.Id == station.Id))
                {
                    throw new IdDoesNotExistException(station.Id);
                }
                int i = 0;
                while(data.StationList[i].Id != station.Id){
                    i++;
                }
                data.StationList[i] = station;
            }

            public void UpdateDrone(Drone drone)
            {
                if (!data.DroneList.Any(x => x.Id == drone.Id))
                {
                    throw new IdDoesNotExistException(drone.Id);
                }
                int i = 0;
                while (data.DroneList[i].Id != drone.Id){
                    i++;
                }
                data.DroneList[i] = drone;
            }

            public void UpdateCustomer(Customer customer)
            {
                if (!data.CustomerList.Any(x => x.Id == customer.Id))
                {
                    throw new IdDoesNotExistException(customer.Id);
                }
                int i = 0;
                while (data.CustomerList[i].Id != customer.Id)
                {
                    i++;
                }
                data.CustomerList[i] = customer;
            }

            public void UpdateParcel(Parcel parcel)
            {
                if (!data.ParcelList.Any(x => x.Id == parcel.Id))
                {
                    throw new IdDoesNotExistException(parcel.Id);
                }
                int i = 0;
                while (data.ParcelList[i].Id != parcel.Id)
                {
                    i++;
                }
                data.ParcelList[i] = parcel;
            }

            public double[] electricityUse()
            {

                return new double[]{ 1, 1, 5, 10, 15 }; //Charging per minute
            }

            public DroneCharge displayDroneCharge(int Id)
            {
                if (!data.DroneChargeList.Any(x => x.droneId == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (DroneCharge item in data.DroneChargeList) { if (item.droneId == Id) { return item; } }
                return new DroneCharge { droneId = -1, StationId = -1 };
            }

            public IEnumerable<DroneCharge> displayDroneChargeList()
            {
                return data.DroneChargeList;
            }

            public void AddDroneCharge(DroneCharge droneCharge)
            {
                if (data.DroneChargeList.Any(x => x.droneId == droneCharge.droneId))
                {
                    throw new IdAlreadyExistException(droneCharge.droneId);
                }
                data.DroneChargeList.Add(droneCharge);
            }

            public void deleteDroneCharge(int Id)
            {
                if (!data.DroneChargeList.Any(x => x.droneId == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                data.DroneChargeList.Remove(displayDroneCharge(Id));
            }
        }
    }
}