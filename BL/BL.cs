using IBL.BO;
using IDAL;
using System;
using System.Collections.Generic;


namespace BL
{
    public class BL : IBL.IBL
    {
        List<DroneToList> droneList = new List<DroneToList>();
        double electricityUseForVacantDrone, electricityUseForLightParcel, electricityUseForMediumParcel, electricityUseForHeavyParcel, chargingRate;
        IDAL.IDal dali = new DAL.DalObject.DalObject();
        static readonly Random randy = new Random();
        public BL()
        {
            double[] electricity = dali.electricityUse();
            electricityUseForVacantDrone = electricity[0]; electricityUseForLightParcel = electricity[1]; electricityUseForMediumParcel = electricity[2]; electricityUseForHeavyParcel = electricity[3]; chargingRate = electricity[4];
            foreach(var drone in dali.displayDroneList())
            {
                DroneToList drone1 = new DroneToList();
                drone1.id = drone.Id;
                drone1.model = drone.Model;
                drone1.maxWeight = (WeightCategories)drone.MaxWeight;
                IDAL.DO.Parcel parcelDidNotDelivered = new IDAL.DO.Parcel();
                parcelDidNotDelivered.Id = -1;
                foreach(var parcel in dali.displayParcelList()){
                    if (parcel.DroneId == drone.Id && parcel.Provided == DateTime.MinValue) {
                        parcelDidNotDelivered = parcel;
                        break;
                    }
                }
                if (parcelDidNotDelivered.Id != -1) {
                    drone1.status = DroneStatus.delivery;
                    if (parcelDidNotDelivered.Assigned != DateTime.MinValue && parcelDidNotDelivered.Collected == DateTime.MinValue) { drone1.location = nearStation(dali.displayCustomer(parcelDidNotDelivered.SenderId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.SenderId).Longitude); }
                    if (parcelDidNotDelivered.Collected != DateTime.MinValue && parcelDidNotDelivered.Provided == DateTime.MinValue) { drone1.location = new Location(dali.displayCustomer(parcelDidNotDelivered.SenderId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.SenderId).Longitude); }
                    double minBattery;
                    Location targetnearStation = nearStation(dali.displayCustomer(parcelDidNotDelivered.TargetId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Longitude);
                    switch ((WeightCategories)parcelDidNotDelivered.Weight)
                    {
                        case WeightCategories.light:
                            minBattery = electricityUseForLightParcel * (DistanceTo(drone1.location.latitude, drone1.location.longitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Longitude) + DistanceTo(dali.displayCustomer(parcelDidNotDelivered.TargetId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Longitude, targetnearStation.latitude, targetnearStation.longitude));
                            break;
                        case WeightCategories.medium:
                            minBattery = electricityUseForMediumParcel * (DistanceTo(drone1.location.latitude, drone1.location.longitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Longitude) + DistanceTo(dali.displayCustomer(parcelDidNotDelivered.TargetId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Longitude, targetnearStation.latitude, targetnearStation.longitude));
                            break;
                        case WeightCategories.liver:
                            minBattery = electricityUseForHeavyParcel * (DistanceTo(drone1.location.latitude, drone1.location.longitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Longitude) + DistanceTo(dali.displayCustomer(parcelDidNotDelivered.TargetId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Longitude, targetnearStation.latitude, targetnearStation.longitude));
                            break;
                        default:
                            minBattery = 0;
                            break;
                    }
                    drone1.battery = randy.Next( (int)minBattery, 100);
                }
                else{
                    int booly = randy.Next(1, 2);
                    if (booly == 1) { drone1.status = DroneStatus.free; }
                    if (booly == 2) { drone1.status = DroneStatus.matance; }
                }
                if(drone1.status == DroneStatus.matance)
                {
                    int numStation = randy.Next(1, stationsAmount());
                    int i = 0;
                    foreach (var station in dali.displayStationList()) { if (i == numStation) { drone1.location = new Location(station.Longitude, station.Lattitude); i++; } }//random location for drone1
                    drone1.battery = randy.Next(0, 20);
                }
                else if(drone1.status == DroneStatus.free)
                {
                    List<IDAL.DO.Parcel> parcelDelivered = listParcelDelivered();
                    int parcelsDelivered = randy.Next(0, parcelDelivered.Count);
                    IDAL.DO.Customer targetCutomer = new IDAL.DO.Customer (dali.displayCustomer(parcelDelivered[parcelsDelivered].TargetId));
                    drone1.location = new Location (targetCutomer.Lattitude, targetCutomer.Longitude);
                    Location targetnearStation = nearStation(targetCutomer.Lattitude, targetCutomer.Longitude);
                    double destinationToNearStation = DistanceTo(drone1.location.latitude, drone1.location.longitude, targetCutomer.Lattitude, targetCutomer.Longitude);
                    drone1.battery = 100 - randy.NextDouble() * (int)electricityUseForVacantDrone*destinationToNearStation ;
                }
                droneList.Add(drone1);
            }
        }

        public void Addcustomer(Customer customer)
        {
            IDAL.DO.Customer DCustomer = new IDAL.DO.Customer { Id = customer.id, Lattitude = customer.location.latitude, Longitude = customer.location.longitude, Name = customer.name, Phone = customer.phone};
            dali.AddCustomer(DCustomer);
        }

        public void AddDrone(Drone drone, int stationId)
        {
            double DBattery = 20 + 20*randy.NextDouble();
            Location Dlocation = new Location(dali.displayStation(stationId).Longitude, dali.displayStation(stationId).Lattitude);
            droneList.Add(new DroneToList() { id = drone.id, maxWeight = drone.maxWeight, battery = DBattery, location = Dlocation});
            IDAL.DO.Drone Ddrone = new IDAL.DO.Drone { Id = drone.id, MaxWeight = (IDAL.DO.WeightCategories) drone.maxWeight, Model = drone.model };
            dali.AddDrone(Ddrone);
        }

        public void AddParcel(Parcel parcel, int senderId, int gettedId)
        {
            IDAL.DO.Parcel DParcel = new IDAL.DO.Parcel { Id = parcel.Id, SenderId = senderId, TargetId = gettedId, Weight = (IDAL.DO.WeightCategories)parcel.weight, Priority = (IDAL.DO.Priorities)parcel.priority };
            dali.AddParcel(DParcel);
        }

        public void AddStation(Station station)
        {
            IDAL.DO.Station Dstation = new IDAL.DO.Station { Id = station.id, Name = station.name, Longitude = station.location.longitude, Lattitude = station.location.latitude, ChargeSlots = station.numFreeChargingStands };
            dali.AddStation(Dstation);
            
        }

        public void assignParcelToDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public void collectParcelByDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public Customer displaycustomer(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerToList> displaycustomerList()
        {
            throw new NotImplementedException();
        }

        public Drone displayDrone(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DroneToList> displayDroneList()
        {
            throw new NotImplementedException();
        }

        public Parcel displayParcel(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParcelToList> displayParcelList()
        {
            throw new NotImplementedException();
        }

        public Station displayStation(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StationToList> displayStationList()
        {
            throw new NotImplementedException();
        }

        public double[] electricityUse()
        {
            throw new NotImplementedException();
        }

        public void provideParcelByDrone(int droneId)
        {
            throw new NotImplementedException();
        }

        public void releaseDroneFromCharging(int droneId, double chargingTime)
        {
            throw new NotImplementedException();
        }

        public void sendDroneToCharging(int droneId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ParcelToList> displayFreeParcelList()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<StationToList> displayFreeStationList()
        {
            throw new NotImplementedException();
        }
        public void Updatecustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude)
        {
            IDAL.DO.Customer customer = new IDAL.DO.Customer { Id = customerId, Name = customerName, Phone = customerPhone, Lattitude = customerLattitude, Longitude = customerLongitude };
            dali.UpdateCustomer(customer);
        }

        public void UpdatecustomerNameAndPhone(int customerId, string customerName, string customerPhone)
        {
            
            IDAL.DO.Customer customer = dali.displayCustomer(customerId);
            customer.Name = customerName;
            customer.Phone = customerPhone;
            dali.UpdateCustomer(customer);
        }

        public void UpdateDrone(int droneId, string droneModel, WeightCategories droneMaxWeight)
        {
            IDAL.DO.Drone drone = new IDAL.DO.Drone { Id = droneId, Model = droneModel, MaxWeight = (IDAL.DO.WeightCategories)droneMaxWeight};
            dali.UpdateDrone(drone);
        }

        public void UpdateDroneModel(int droneId, string droneModel)
        {

            IDAL.DO.Drone drone = dali.displayDrone(droneId);
            drone.Model = droneModel;
            dali.UpdateDrone(drone);

        }

        public void UpdateParcel(int parcelId, int senderId, int targetId, WeightCategories parcelWeight, Priorities priority)
        {
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel { Id = parcelId, SenderId = senderId, TargetId = targetId, Weight = (IDAL.DO.WeightCategories)parcelWeight, Priority = (IDAL.DO.Priorities)priority,  };
            dali.UpdateParcel(parcel);
        }

        public void UpdateStation(int stationId, int stationName, double stationLongitude, double stationLattitude, int chargeSlots)
        {
            IDAL.DO.Station station = new IDAL.DO.Station { Id = stationId, Name = stationName, Longitude = stationLongitude, Lattitude = stationLattitude, ChargeSlots = chargeSlots };
            dali.UpdateStation(station);
        }

        public void UpdateStationName(int stationId, int stationName)
        {
            IDAL.DO.Station station = dali.displayStation(stationId);
            station.Name = stationName;
            dali.UpdateStation(station);
        }

        Location nearStation(double latitude, double longitude)
        {
            IDAL.DO.Station nearStation = new IDAL.DO.Station();
            double distance = double.MaxValue;
            foreach(var station in dali.displayStationList())
            {
                double newDistance = DistanceTo(latitude, longitude, station.Lattitude, station.Longitude);// Math.Sqrt(Math.Pow((latitude - station.Lattitude), 2) + Math.Pow((longitude - station.Longitude), 2));
                if (distance> newDistance) {
                    distance = newDistance;
                    nearStation = station;
                }
            }
            Location location1 = new Location(nearStation.Longitude, nearStation.Lattitude);
            return location1;
        }
        double DistanceTo(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;
            return dist * 1.609344;
        }

        List<IDAL.DO.Parcel> listParcelDelivered()
        {
            List<IDAL.DO.Parcel> parcelDelivered = new List<IDAL.DO.Parcel>();
            foreach(var parcel in dali.displayParcelList())
            {
                if(parcel.Provided != DateTime.MinValue)
                {
                    parcelDelivered.Add(parcel);
                }
            }
            return parcelDelivered;
        }

        int stationsAmount()
        {
            int counter = 0;
            foreach(var station in dali.displayStationList())
            {
                counter++;
            }
            return counter;
        }
    }
}
