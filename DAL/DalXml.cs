using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace DAL
{
    interface DalXml
    {
        public class DalXml : IDal
        {

            XmlSerializer droSer = new XmlSerializer(typeof(List<Drone>));
            XmlReader droReader = new XmlTextReader(@"Data\Drones.xml");
            List<Drone> droData;
            XmlSerializer staSer = new XmlSerializer(typeof(List<Station>));
            XmlReader staReader = new XmlTextReader(@"Data\Stations.xml");
            List<Station> staData;
            XmlSerializer cusSer = new XmlSerializer(typeof(List<Customer>));
            XmlReader cusReader = new XmlTextReader(@"Data\Customers.xml");
            List<Customer> cusData;
            XmlSerializer parSer = new XmlSerializer(typeof(List<Parcel>));
            XmlReader parReader = new XmlTextReader(@"Data\Parcels.xml");
            List<Parcel> parData;
            XmlSerializer droCharSer = new XmlSerializer(typeof(List<DroneCharge>));
            XmlReader droCharReader = new XmlTextReader(@"Data\DroneCharges.xml");
            List<DroneCharge> droCharData;

            TextWriter cusWriter = new StreamWriter(@"Data\Customers.xml");
            TextWriter droWriter = new StreamWriter(@"Data\Drones.xml");
            TextWriter staWriter = new StreamWriter(@"Data\Stations.xml");
            TextWriter parWriter = new StreamWriter(@"Data\Parcels.xml");
            TextWriter droCharWriter = new StreamWriter(@"Data\DroneCharges.xml");
            //singelton
            
            static DalXml() { }
            private static DalXml instace;
            static readonly object lockname = new object();
            public static DalXml Instace
            {
                get
                {
                    if (instace == null)
                    {
                        lock (lockname)
                        {
                            if (instace == null)
                            {
                                instace = new DalXml();
                            }
                        }
                    }
                    return instace;
                }
            }
            public void AddCustomer(Customer customer)
            {
                cusData = (List<Customer>)cusSer.Deserialize(cusReader);
                cusReader.Close();
                if (cusData.Any(x => x.Id == customer.Id))
                {
                    throw new IdAlreadyExistException(customer.Id);
                }
                cusData.Add(customer);

                cusSer.Serialize(cusWriter, cusData);
                cusWriter.Close();
            }

            public void AddDrone(Drone drone)
            {
                droData = (List<Drone>)droSer.Deserialize(droReader);
                droReader.Close();
                if (droData.Any(x => x.Id == drone.Id))
                {
                    throw new IdAlreadyExistException(drone.Id);
                }
                droData.Add(drone);

                droSer.Serialize(droWriter, droData);
                droWriter.Close();
            }

            public void AddDroneCharge(DroneCharge droneCharge)
            {
                droCharData = (List<DroneCharge>)droCharSer.Deserialize(droCharReader);
                droCharReader.Close();
                if (droCharData.Any(x => x.droneId == droneCharge.droneId))
                {
                    throw new IdAlreadyExistException(droneCharge.droneId);
                }
                droCharData.Add(droneCharge);

                droCharSer.Serialize(droCharWriter, droCharData);
                droCharWriter.Close();
            }

            public void AddParcel(Parcel parcel)
            {
                parData = (List<Parcel>)parSer.Deserialize(parReader);
                parReader.Close();
                if (parData.Any(x => x.Id == parcel.Id))
                {
                    throw new IdAlreadyExistException(parcel.Id);
                }
                parData.Add(parcel);

                parSer.Serialize(parWriter, parData);
                parWriter.Close();
            }

            public void AddStation(Station station)
            {
                staData = (List<Station>)staSer.Deserialize(staReader);
                staReader.Close();
                if (staData.Any(x => x.Id == station.Id))
                {
                    throw new IdAlreadyExistException(station.Id);
                }
                staData.Add(station);

                staSer.Serialize(staWriter, staData);
                staWriter.Close();
            }

            public void deleteDroneCharge(int Id)
            {
                droCharData = (List<DroneCharge>)droCharSer.Deserialize(droCharReader);
                staReader.Close();
                if (!droCharData.Any(x => x.droneId == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                droCharData.Remove(displayDroneCharge(Id));
                staSer.Serialize(staWriter, staData);
                staWriter.Close();
            }

            public Customer displayCustomer(int Id)
            {
                cusData = (List <Customer>) cusSer.Deserialize(cusReader);
                if (!cusData.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Customer item in cusData) { if (item.Id == Id) { return item; } }
                return new Customer { Id = -1, Name = "None", Phone = "0", Longitude = 0, Lattitude = 0 };
            }
            public Drone displayDrone(int Id)
            {
                droData = (List<Drone>)droSer.Deserialize(droReader);
                if (!droData.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Drone item in droData) { if (item.Id == Id) { return item; } }
                return new Drone { Id = -1, Model = "None", MaxWeight = WeightCategories.light };
            }
            public DroneCharge displayDroneCharge(int Id)
            {
                if (!droCharData.Any(x => x.droneId == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (DroneCharge item in droCharData) { if (item.droneId == Id) { return item; } }
                return new DroneCharge { droneId = -1, StationId = -1 };
            }

            public Parcel displayParcel(int Id)
            {
                if (!parData.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Parcel item in parData) { if (item.Id == Id) { return item; } }
                return new Parcel { Id = -1, SenderId = -1, TargetId = -1, Weight = WeightCategories.light, Priority = Priorities.regular, Defined = DateTime.Now, Assigned = DateTime.Now, Collected = DateTime.Now, Provided = DateTime.Now, DroneId = -1 };
            }
            public Station displayStation(int Id)
            {
                if (!staData.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Station item in staData) { if (item.Id == Id) { return item; } }
                return new Station { Id = -1, Name = -1, Longitude = -1, Lattitude = -1, freeChargeSlots = -1 };
            }
            public Station displayStationByLocation(double latitude, double longitude)
            {
                if (!staData.Any(x => x.Lattitude == latitude && x.Longitude == longitude))
                {
                    throw new LocationDoesNotExistException();
                }
                foreach (Station item in staData) { if (item.Longitude == longitude && item.Lattitude == latitude) { return item; } }
                return new Station { Id = -1, Name = -1, Longitude = -1, Lattitude = -1, freeChargeSlots = -1 };
            }

            public IEnumerable<Customer> displayCustomerList(){ return cusData; }


            public IEnumerable<DroneCharge> displayDroneChargeList() { return droCharData; }

            public IEnumerable<Drone> displayDroneList() { return droData; }


            public IEnumerable<Parcel> displayParcelList() { return parData; }

            public IEnumerable<Parcel> displayParcelList(Predicate<Parcel> predicate) { return parData.FindAll(predicate); }



            public IEnumerable<Station> displayStationList() { return staData; }

            public double[] electricityUse()
            {
                return new double[] { 1, 1, 5, 10, 15 }; //Charging per minute
            }

            public void UpdateCustomer(Customer customer)
            {
                cusData = (List<Customer>)cusSer.Deserialize(cusReader);
                cusReader.Close();
                if (!cusData.Any(x => x.Id == customer.Id))
                {
                    throw new IdDoesNotExistException(customer.Id);
                }
                int i = 0;
                while (cusData[i].Id != customer.Id)
                {
                    i++;
                }
                cusData[i] = customer;
                cusSer.Serialize(cusWriter, cusData);
                cusWriter.Close();
            }

            public void UpdateDrone(Drone drone)
            {
                droData = (List<Drone>)droSer.Deserialize(droReader);
                droReader.Close();
                if (!droData.Any(x => x.Id == drone.Id))
                {
                    throw new IdDoesNotExistException(drone.Id);
                }
                int i = 0;
                while (droData[i].Id != drone.Id)
                {
                    i++;
                }
                droData[i] = drone;
                droSer.Serialize(droWriter, droData);
                droWriter.Close();
            }

            public void UpdateParcel(Parcel parcel)
            {
                parData = (List<Parcel>)parSer.Deserialize(parReader);
                parReader.Close();
                if (!parData.Any(x => x.Id == parcel.Id))
                {
                    throw new IdDoesNotExistException(parcel.Id);
                }
                int i = 0;
                while (parData[i].Id != parcel.Id)
                {
                    i++;
                }
                parData[i] = parcel;
                parSer.Serialize(parWriter, parData);
                parWriter.Close();
            }

            public void UpdateStation(Station station)
            {
                staData = (List<Station>)staSer.Deserialize(staReader);
                staReader.Close();
                if (!staData.Any(x => x.Id == station.Id))
                {
                    throw new IdDoesNotExistException(station.Id);
                }
                int i = 0;
                while (staData[i].Id != station.Id)
                {
                    i++;
                }
                staData[i] = station;
                staSer.Serialize(staWriter, staData);
                staWriter.Close();
            }
        }
    }
}
