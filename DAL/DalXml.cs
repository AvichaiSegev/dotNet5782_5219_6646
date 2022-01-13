using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;
using System.Runtime.CompilerServices;

namespace DAL
{
    interface DalXml
    {
        public class DalXml : IDal
        {

            XmlSerializer droSer = new XmlSerializer(typeof(List<Drone>));
            List<Drone> droData;
            XmlSerializer staSer = new XmlSerializer(typeof(List<Station>));
            List<Station> staData;
            XmlSerializer cusSer = new XmlSerializer(typeof(List<Customer>));
            List<Customer> cusData;
            XmlSerializer parSer = new XmlSerializer(typeof(List<Parcel>));
            List<Parcel> parData;
            XmlSerializer droCharSer = new XmlSerializer(typeof(List<Parcel>));
            List<DroneCharge> droCharData;


            //XmlSerializer droCharSer = new XmlSerializer(typeof(List<DroneCharge>));
            //XmlReader droCharReader = new XmlTextReader(@"Data\DroneCharges.xml");
            //List<DroneCharge> droCharData;

            

            //singelton

            static DalXml() { }

            public DalXml()
            {
                XElement droCharData = droCharData = new XElement("DroneCharge", new XElement[] { });

                droCharData.Save(@"Data\DroneCharges.xml");
                //XmlReader droReader = new XmlTextReader(@"Data\Drones.xml");
                //droData = (List<Drone>)droSer.Deserialize(droReader);
                //droReader.Close();
            }

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
                XmlReader cusReader = new XmlTextReader(@"Data\Customers.xml");
                cusData = (List<Customer>)cusSer.Deserialize(cusReader);
                cusReader.Close();
                if (cusData.Any(x => x.Id == customer.Id))
                {
                    throw new IdAlreadyExistException(customer.Id);
                }
                cusData.Add(customer);

                TextWriter cusWriter = new StreamWriter(@"Data\Customers.xml");
                cusSer.Serialize(cusWriter, cusData);
                cusWriter.Close();
            }

            
            public void AddDrone(Drone drone)
            {
                XmlReader droReader = new XmlTextReader(@"Data\Drones.xml");
                droData = (List<Drone>)droSer.Deserialize(droReader);
                droReader.Close();
                if (droData.Any(x => x.Id == drone.Id))
                {
                    throw new IdAlreadyExistException(drone.Id);
                }
                droData.Add(drone);

                TextWriter droWriter = new StreamWriter(@"Data\Drones.xml");
                droSer.Serialize(droWriter, droData);
                droWriter.Close();
            }

            
            public void AddDroneCharge(DroneCharge droneCharge)
            {
                XElement droCharData = XElement.Load(@"Data\DroneCharges.xml");

                if (droCharData.Elements().Any(x => int.Parse(x.Element("droneId").Value) == droneCharge.droneId))
                {
                    throw new IdAlreadyExistException(droneCharge.droneId);
                }
                droCharData.Add(new XElement("DroneCharge", new XElement[] {
                    new XElement("droneId", droneCharge.droneId),
                    new XElement("StationId", droneCharge.StationId)
                }));

                droCharData.Save(@"Data\DroneCharges.xml");
            }
            
            public void AddParcel(Parcel parcel)
            {
                XmlReader parReader = new XmlTextReader(@"Data\Parcels.xml");
                parData = (List<Parcel>)parSer.Deserialize(parReader);
                parReader.Close();
                if (parData.Any(x => x.Id == parcel.Id))
                {
                    throw new IdAlreadyExistException(parcel.Id);
                }
                parData.Add(parcel);

                TextWriter parWriter = new StreamWriter(@"Data\Parcels.xml");
                parSer.Serialize(parWriter, parData);
                parWriter.Close();
            }
            
            public void AddStation(Station station)
            {
                XmlReader staReader = new XmlTextReader(@"Data\Stations.xml");
                staData = (List<Station>)staSer.Deserialize(staReader);
                staReader.Close();
                if (staData.Any(x => x.Id == station.Id))
                {
                    throw new IdAlreadyExistException(station.Id);
                }
                staData.Add(station);

                TextWriter staWriter = new StreamWriter(@"Data\Stations.xml");
                staSer.Serialize(staWriter, staData);
                staWriter.Close();
            }

            
            public void deleteDroneCharge(int Id)
            {
                XElement droCharData = XElement.Load(@"Data\DroneCharges.xml");

                if (!droCharData.Elements().Any(x => int.Parse(x.Element("droneId").Value) == Id))
                {
                    throw new IdAlreadyExistException(Id);
                }
                droCharData.Elements().Where(el => int.Parse(el.Element("droneId").Value) == Id).Remove();

                droCharData.Save(@"Data\DroneCharges.xml");
            }

            
            public Customer displayCustomer(int Id)
            {
                XmlReader cusReader = new XmlTextReader(@"Data\Customers.xml");
                cusData = (List<Customer>)cusSer.Deserialize(cusReader);
                cusReader.Close();
                if (!cusData.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Customer item in cusData) { if (item.Id == Id) { return item; } }
                return new Customer { Id = -1, Name = "None", Phone = "0", Longitude = 0, Lattitude = 0 };
            }
            
            public Drone displayDrone(int Id)
            {
                XmlReader droReader = new XmlTextReader(@"Data\Drones.xml");
                droData = (List<Drone>)droSer.Deserialize(droReader);
                droReader.Close();
                if (!droData.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Drone item in droData) { if (item.Id == Id) { return item; } }
                return new Drone { Id = -1, Model = "None", MaxWeight = WeightCategories.light };
            }
            
            public DroneCharge displayDroneCharge(int Id)
            {
                XElement droCharData = XElement.Load(@"Data\DroneCharges.xml");
                if (!droCharData.Elements().Any(x => int.Parse(x.Element("droneId").Value) == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                XElement xes = droCharData.Elements().Where(el => int.Parse(el.Element("droneId").Value) == Id).First();
                return new DroneCharge() { droneId = int.Parse(xes.Element("droneId").Value), StationId = int.Parse(xes.Element("stationId").Value) };

                droCharData.Save(@"Data\DroneCharges.xml");
            }

            
            public Parcel displayParcel(int Id)
            {
                XmlReader parReader = new XmlTextReader(@"Data\Parcels.xml");
                parData = (List<Parcel>)parSer.Deserialize(parReader);
                parReader.Close();
                if (!parData.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Parcel item in parData) { if (item.Id == Id) { return item; } }
                return new Parcel { Id = -1, SenderId = -1, TargetId = -1, Weight = WeightCategories.light, Priority = Priorities.regular, Defined = DateTime.Now, Assigned = DateTime.Now, Collected = DateTime.Now, Provided = DateTime.Now, DroneId = -1 };
            }
            
            public Station displayStation(int Id)
            {
                XmlReader staReader = new XmlTextReader(@"Data\Stations.xml");
                staData = (List<Station>)staSer.Deserialize(staReader);
                staReader.Close();
                if (!staData.Any(x => x.Id == Id))
                {
                    throw new IdDoesNotExistException(Id);
                }
                foreach (Station item in staData) { if (item.Id == Id) { return item; } }
                return new Station { Id = -1, Name = -1, Longitude = -1, Lattitude = -1, freeChargeSlots = -1 };
            }
            
            public Station displayStationByLocation(double latitude, double longitude)
            {
                XmlReader staReader = new XmlTextReader(@"Data\Stations.xml");
                staData = (List<Station>)staSer.Deserialize(staReader);
                staReader.Close();
                if (!staData.Any(x => x.Lattitude == latitude && x.Longitude == longitude))
                {
                    throw new LocationDoesNotExistException();
                }
                foreach (Station item in staData) { if (item.Longitude == longitude && item.Lattitude == latitude) { return item; } }
                return new Station { Id = -1, Name = -1, Longitude = -1, Lattitude = -1, freeChargeSlots = -1 };
            }

            
            public IEnumerable<Customer> displayCustomerList(){
                XmlReader cusReader = new XmlTextReader(@"Data\Customers.xml");
                cusData = (List<Customer>)cusSer.Deserialize(cusReader);
                cusReader.Close();
                return cusData; }


            
            public IEnumerable<DroneCharge> displayDroneChargeList()
            {
                XElement droCharData = XElement.Load(@"Data\DroneCharges.xml");
                return droCharData.Elements().Select(p => new DroneCharge() { droneId = int.Parse(p.Element("droneId").Value), StationId = int.Parse(p.Element("stationId").Value) });
            }

            
            public IEnumerable<Drone> displayDroneList()
            {
                XmlReader droReader = new XmlTextReader(@"Data\Drones.xml");
                droData = (List<Drone>)droSer.Deserialize(droReader);
                droReader.Close();
                return droData; }

            
            public IEnumerable<Parcel> displayParcelList() {
                XmlReader parReader = new XmlTextReader(@"Data\Parcels.xml");
                parData = (List<Parcel>)parSer.Deserialize(parReader);
                parReader.Close();
                return parData; }

            
            public IEnumerable<Parcel> displayParcelList(Predicate<Parcel> predicate) {
                XmlReader parReader = new XmlTextReader(@"Data\Parcels.xml");
                parData = (List<Parcel>)parSer.Deserialize(parReader);
                parReader.Close();
                return parData.FindAll(predicate); }



            
            public IEnumerable<Station> displayStationList() {
                XmlReader staReader = new XmlTextReader(@"Data\Stations.xml");
                staData = (List<Station>)staSer.Deserialize(staReader);
                staReader.Close();
                return staData; }

            
            public double[] electricityUse()
            {
                return new double[] { 1, 1, 5, 10, 15 }; //Charging per minute
            }

            
            public void UpdateCustomer(Customer customer)
            {
                XmlReader cusReader = new XmlTextReader(@"Data\Customers.xml");
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
                TextWriter cusWriter = new StreamWriter(@"Data\Customers.xml");
                cusSer.Serialize(cusWriter, cusData);
                cusWriter.Close();
            }

            
            public void UpdateDrone(Drone drone)
            {
                XmlReader droReader = new XmlTextReader(@"Data\Drones.xml");
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
                TextWriter droWriter = new StreamWriter(@"Data\Drones.xml");
                droSer.Serialize(droWriter, droData);
                droWriter.Close();
            }

            
            public void UpdateParcel(Parcel parcel)
            {
                XmlReader parReader = new XmlTextReader(@"Data\Parcels.xml");
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
                TextWriter parWriter = new StreamWriter(@"Data\Parcels.xml");
                parSer.Serialize(parWriter, parData);
                parWriter.Close();
            }

            
            public void UpdateStation(Station station)
            {
                XmlReader staReader = new XmlTextReader(@"Data\Stations.xml");
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
                TextWriter staWriter = new StreamWriter(@"Data\Stations.xml");
                staSer.Serialize(staWriter, staData);
                staWriter.Close();
            }
        }
    }
}
