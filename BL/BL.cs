using IBL.BO;
using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    IDAL.DO.Customer customerTarget = dali.displayCustomer(parcelDidNotDelivered.TargetId);
                    switch ((WeightCategories)parcelDidNotDelivered.Weight)
                    {
                        case WeightCategories.light:
                            minBattery = electricityUseForLightParcel * (DistanceTo(drone1.location.latitude, drone1.location.longitude, customerTarget.Lattitude, customerTarget.Longitude) + DistanceTo(customerTarget.Lattitude, customerTarget.Longitude, targetnearStation.latitude, targetnearStation.longitude));
                            break;
                        case WeightCategories.medium:
                            minBattery = electricityUseForMediumParcel * (DistanceTo(drone1.location.latitude, drone1.location.longitude, customerTarget.Lattitude, customerTarget.Longitude) + DistanceTo(customerTarget.Lattitude, customerTarget.Longitude, targetnearStation.latitude, targetnearStation.longitude));
                            break;
                        case WeightCategories.liver:
                            minBattery = electricityUseForHeavyParcel * (DistanceTo(drone1.location.latitude, drone1.location.longitude, customerTarget.Lattitude, customerTarget.Longitude) + DistanceTo(customerTarget.Lattitude, customerTarget.Longitude, targetnearStation.latitude, targetnearStation.longitude));
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
            try
            {
                double DBattery = 20 + 20 * randy.NextDouble();
                Location Dlocation = new Location(dali.displayStation(stationId).Longitude, dali.displayStation(stationId).Lattitude);

                droneList.Add(new DroneToList() { id = drone.id, maxWeight = drone.maxWeight, battery = DBattery, location = Dlocation, status = DroneStatus.matance, model = drone.model });
                IDAL.DO.Drone Ddrone = new IDAL.DO.Drone { Id = drone.id, MaxWeight = (IDAL.DO.WeightCategories)drone.maxWeight, Model = drone.model };
                dali.AddDrone(Ddrone);
            }
            catch (IDAL.DO.IdDoesNotExistException err)
            {
                throw new IdDoesNotExist(err.ID);
            }
        }

        public void AddParcel(Parcel parcel, int senderId, int gettedId)
        {
            IDAL.DO.Parcel DParcel = new IDAL.DO.Parcel { Id = parcel.Id, SenderId = senderId, TargetId = gettedId, Weight = (IDAL.DO.WeightCategories)parcel.weight, Priority = (IDAL.DO.Priorities)parcel.priority , DroneId = int.MinValue, Defined = DateTime.Now, Assigned = DateTime.MinValue, Collected = DateTime.MinValue, Provided = DateTime.MinValue};
            dali.AddParcel(DParcel);
        }

        public void AddStation(Station station)
        {
            IDAL.DO.Station Dstation = new IDAL.DO.Station { Id = station.id, Name = station.name, Longitude = station.location.longitude, Lattitude = station.location.latitude, freeChargeSlots = station.numFreeChargingStands };
            dali.AddStation(Dstation);
            
        }

        public void assignParcelToDrone(int droneId)
        {
            Drone drone = displayDrone(droneId);
            Parcel parcel = assignAParcelToDrone(drone, Priorities.emergency);
            if (parcel.Id == -1)
            {
                parcel = assignAParcelToDrone(drone, Priorities.quick);
                if(parcel.Id == -1)
                {
                    parcel = assignAParcelToDrone(drone, Priorities.regular);
                    if(parcel.Id == -1) { throw new NoSuiTablePackageFound(); }
                }
            }
            UpdateDrone(new Drone() { id = drone.id, battery = drone.battery, location = drone.location, maxWeight = drone.maxWeight, model = drone.model, status = DroneStatus.delivery });
            parcel.droneInParcel = new DroneInParcel() { battery = drone.battery, id = drone.id, location = drone.location };
            parcel.assignedParcelTime = DateTime.Now;
            UpdateParcel(parcel);
        }
        Parcel assignAParcelToDrone(Drone drone, Priorities priority)
        {
            Parcel Bparcel = new Parcel(){ Id = -1};
            double closestDistance = double.MaxValue;
            foreach(var parcel in dali.displayParcelList())
            {
                if (parcel.Weight <= (IDAL.DO.WeightCategories)drone.maxWeight)
                {
                    Location senderL = new Location(dali.displayCustomer(parcel.SenderId).Longitude, dali.displayCustomer(parcel.SenderId).Lattitude);
                    Location targetL = new Location(dali.displayCustomer(parcel.TargetId).Longitude, dali.displayCustomer(parcel.TargetId).Lattitude);
                    Location nearTargetStation = nearStation(targetL.latitude, targetL.longitude);
                    double totalDistance = DistanceTo(drone.location.latitude, drone.location.longitude, senderL.latitude, senderL.longitude) + DistanceTo(senderL.latitude, senderL.longitude, targetL.latitude, targetL.longitude) + DistanceTo(targetL.latitude, targetL.longitude, nearTargetStation.latitude, nearTargetStation.longitude);
                    double minBattery;
                    switch ((WeightCategories)parcel.Weight)
                    {
                        case WeightCategories.light:
                            minBattery = electricityUseForLightParcel * totalDistance;
                            break;
                        case WeightCategories.medium:
                            minBattery = electricityUseForMediumParcel * totalDistance;
                            break;
                        case WeightCategories.liver:
                            minBattery = electricityUseForHeavyParcel * totalDistance;
                            break;
                        default:
                            minBattery = 0;
                            break;
                    }
                    if (minBattery <= drone.battery)
                    {
                        double distanceToParcel = DistanceTo(drone.location.latitude, drone.location.longitude, senderL.latitude, senderL.longitude);
                        if (distanceToParcel < closestDistance)
                        {
                            Bparcel.Id = parcel.Id;
                            Bparcel.priority = (Priorities)parcel.Priority;
                            Bparcel.providedParcelTime = parcel.Provided;
                            Bparcel.delivered.id = parcel.SenderId;
                            Bparcel.getted.id = parcel.TargetId;
                            Bparcel.weight = (WeightCategories)parcel.Weight;
                            Bparcel.droneInParcel.id = parcel.DroneId;
                            Bparcel.definedParcelTime = parcel.Defined;
                            Bparcel.collectedParcelTime = parcel.Collected;
                            Bparcel.assignedParcelTime = parcel.Assigned;
                            closestDistance = distanceToParcel;
                        }
                    }
                }
            }
            return Bparcel;
        }

        public void collectParcelByDrone(int droneId)
        {
            DroneToList droneToList = displayDroneToList(droneId);
            Parcel parcel = displayParcel(droneToList.parcelNumber);
            Customer customer = displaycustomer(parcel.delivered.id);
            if (droneToList.status != DroneStatus.delivery || parcel.droneInParcel.id != droneId || parcel.assignedParcelTime == DateTime.MinValue || parcel.collectedParcelTime != DateTime.MinValue)
            {
                throw new DroneDoesNotSuitable();
            }
            double distance = DistanceTo(droneToList.location.latitude, droneToList.location.longitude, customer.location.latitude, customer.location.longitude);
            droneToList.battery = droneToList.battery - electricityUseForVacantDrone * distance;
            droneToList.location = customer.location;
            UpdateDrone(new Drone() { battery = droneToList.battery, id = droneToList.id, location = droneToList.location, maxWeight = (WeightCategories)droneToList.maxWeight, model = droneToList.model, parcel = new ParcelInTransfer(), status = droneToList.status });
            parcel.collectedParcelTime = DateTime.Now;
            UpdateParcel(parcel);
        }

        public void provideParcelByDrone(int droneId)
        {
            DroneToList droneToList = displayDroneToList(droneId);
            Parcel parcel = displayParcel(droneToList.parcelNumber);
            Customer customer = displaycustomer(parcel.getted.id);
            if (droneToList.status != DroneStatus.delivery || parcel.droneInParcel.id != droneId || parcel.collectedParcelTime == DateTime.MinValue || parcel.providedParcelTime != DateTime.MinValue)
            {
                throw new DroneDoesNotSuitable();
            }
            double distance = DistanceTo(droneToList.location.latitude, droneToList.location.longitude, customer.location.latitude, customer.location.longitude);
            switch ((WeightCategories)parcel.weight)
            {
                case WeightCategories.light:
                    droneToList.battery = droneToList.battery - electricityUseForLightParcel * distance;
                    break;
                case WeightCategories.medium:
                    droneToList.battery = droneToList.battery - electricityUseForMediumParcel * distance;
                    break;
                case WeightCategories.liver:
                    droneToList.battery = droneToList.battery - electricityUseForHeavyParcel * distance;
                    break;
                default:
                    droneToList.battery = 0;
                    break;
            }
            droneToList.location = customer.location;
            UpdateDrone(new Drone() { battery = droneToList.battery, id = droneToList.id, location = droneToList.location, maxWeight = (WeightCategories)droneToList.maxWeight, model = droneToList.model, parcel = new ParcelInTransfer(), status = droneToList.status });
            parcel.providedParcelTime = DateTime.Now;
            UpdateParcel(parcel);
        }

        public Customer displaycustomer(int Id)
        {
            IDAL.DO.Customer customer1 = dali.displayCustomer(Id);
            Customer customer2 = new Customer();
            customer2.id = customer1.Id;
            customer2.name = customer1.Name;
            customer2.phone = customer1.Phone;
            customer2.location = new Location(customer1.Longitude, customer1.Lattitude);
            return customer2;
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
            IDAL.DO.Parcel parcel1 = dali.displayParcel(Id);
            Parcel parcel2 = new Parcel();
            parcel2.Id = parcel1.Id;
            switch ((WeightCategories)parcel1.Weight)
            {
                case WeightCategories.light:
                    parcel1.Weight = IDAL.DO.WeightCategories.light;
                    break;
                case WeightCategories.medium:
                    parcel1.Weight = IDAL.DO.WeightCategories.medium;
                    break;
                case WeightCategories.liver:
                    parcel1.Weight = IDAL.DO.WeightCategories.liver;
                    break;
                default:break;
            }
            switch ((Priorities)parcel1.Priority)
            {
                case Priorities.emergency:
                    parcel1.Priority = IDAL.DO.Priorities.emergency;
                    break;
                case Priorities.quick:
                    parcel1.Priority = IDAL.DO.Priorities.quick;
                    break;
                case Priorities.regular:
                    parcel1.Priority = IDAL.DO.Priorities.regular;
                    break;
                default: break;
            }
            parcel2.assignedParcelTime = parcel1.Assigned;
            parcel2.collectedParcelTime = parcel1.Collected;
            parcel2.definedParcelTime = parcel1.Defined;
            parcel2.providedParcelTime = parcel1.Provided;
            Customer customer = displaycustomer(parcel1.TargetId);
            parcel2.getted = new CustomerInParcel() { id = customer.id, name = customer.name };
            customer = displaycustomer(parcel1.SenderId);
            parcel2.delivered = new CustomerInParcel() { id = customer.id, name = customer.name };
            Drone drone = displayDrone(parcel1.DroneId);
            parcel2.droneInParcel = new DroneInParcel() { id = drone.id, battery = drone.battery, location = drone.location };
            return parcel2;
        }

        public IEnumerable<ParcelToList> displayParcelList()
        {
            throw new NotImplementedException();
        }

        public Station displayStation(int Id)
        {
            IDAL.DO.Station station1 = dali.displayStation(Id);
            Station station2 = new Station();
            station2.id = station1.Id;
            station2.location = new Location(station1.Longitude, station1.Lattitude);
            station2.name = station1.Name;
            station2.numFreeChargingStands = station1.freeChargeSlots;
            return station2;
        }

        public IEnumerable<StationToList> displayStationList()
        {
            throw new NotImplementedException();
        }

        public double[] electricityUse()
        {
            throw new NotImplementedException();
        }


        DroneToList displayDroneToList(int droneId)
        {
            if (!droneList.Any(x => x.id == droneId))
            {
                throw new IdDoesNotExist(droneId);
            }
            return droneList.Find(x => x.id == droneId);
        }
        public void sendDroneToCharging(int droneId)
        {
            if (displayDroneToList(droneId).status != DroneStatus.free) { throw new dronesStatusIsNotApplicable(); }
            Drone drone = displayDrone(droneId);
            Location nearstation = nearStation(drone.location.latitude, drone.location.longitude);
            double distance = DistanceTo(drone.location.latitude, drone.location.longitude, nearstation.latitude, nearstation.longitude);
            double distanceAbility = drone.battery/electricityUseForVacantDrone;
            if (distanceAbility < distance) { throw new dontHaveMuchBattery(); }
            UpdateDrone(new Drone() { id = drone.id, battery = drone.battery - (distance * electricityUseForVacantDrone), location = nearstation, maxWeight = drone.maxWeight, model = drone.model, status = DroneStatus.matance });
            IDAL.DO.Station station = dali.displayStationByLocation(nearstation.latitude, nearstation.longitude);
            UpdateStation(new Station() { id = station.Id, name = station.Name, location = new Location(station.Longitude, station.Lattitude), numFreeChargingStands = station.freeChargeSlots - 1 });
            dali.AddDroneCharge(new IDAL.DO.DroneCharge() { droneId = drone.id, StationId = station.Id });
        }

        public void releaseDroneFromCharging(int droneId, double chargingTime)
        {
            if (displayDroneToList(droneId).status != DroneStatus.matance) { throw new dronesStatusIsNotApplicable(); }
            Drone drone = displayDrone(droneId);
            UpdateDrone(new Drone() { id = drone.id, battery = drone.battery + (chargingTime * chargingRate), location = drone.location, maxWeight = drone.maxWeight, model = drone.model, status = DroneStatus.free });
            IDAL.DO.Station station = dali.displayStationByLocation(drone.location.latitude, drone.location.longitude);
            UpdateStation(new Station() { id = station.Id, name = station.Name, location = new Location(station.Longitude, station.Lattitude), numFreeChargingStands = station.freeChargeSlots + 1 });
            dali.deleteDroneCharge(droneId);
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

        public void UpdatecustomerNameAndPhone(Customer customer)
        {
            
            IDAL.DO.Customer Dcustomer = dali.displayCustomer(customer.id);
            Dcustomer.Name = customer.name;
            Dcustomer.Phone = customer.phone;
            dali.UpdateCustomer(Dcustomer);
        }

        public void UpdateDrone(Drone drone)
        {
            //////////////////////////////////////////////////////////////I need update the dronelist
            IDAL.DO.Drone Ddrone = new IDAL.DO.Drone() { MaxWeight = (IDAL.DO.WeightCategories)drone.maxWeight, Model = drone.model };
            dali.UpdateDrone(Ddrone);
        }

        public void UpdateDroneModel(Drone drone)
        {

            IDAL.DO.Drone Ddrone = dali.displayDrone(drone.id);
            Ddrone.Model = drone.model;//dali.displayDrone(droneId);
            dali.UpdateDrone(Ddrone);

        }

        public void UpdateParcel(Parcel parcel)
        {
            IDAL.DO.Parcel Dparcel = dali.displayParcel(parcel.Id);
            Dparcel.Priority = (IDAL.DO.Priorities)parcel.priority;
            Dparcel.Provided = parcel.providedParcelTime;
            Dparcel.SenderId = parcel.delivered.id;
            Dparcel.TargetId = parcel.getted.id;
            Dparcel.Weight = (IDAL.DO.WeightCategories)parcel.weight;
            Dparcel.DroneId = parcel.droneInParcel.id;
            Dparcel.Defined = parcel.definedParcelTime;
            Dparcel.Collected = parcel.collectedParcelTime;
            Dparcel.Assigned = parcel.assignedParcelTime;
            dali.UpdateParcel(Dparcel);
        }

        public void UpdateStation(Station station)
        {
            IDAL.DO.Station Dstation = dali.displayStation(station.id);
            Dstation.freeChargeSlots = station.numFreeChargingStands;
            Dstation.Lattitude = station.location.latitude;
            Dstation.Longitude = station.location.longitude;
            Dstation.Name = station.name;
            dali.UpdateStation(Dstation);
        }

        public void UpdateStationName(Station station)
        {
            IDAL.DO.Station Dstation = dali.displayStation(station.id);
            Dstation.Name = station.name;
            dali.UpdateStation(Dstation);
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
            return (dist * 1.609344)/1000;
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

        List<IDAL.DO.Station> listFreeStation()
        {
            List<IDAL.DO.Station> stationFree = new List<IDAL.DO.Station>();
            foreach(var station in dali.displayStationList())
            {
                if (station.freeChargeSlots != 0)
                {
                    stationFree.Add(station);
                }
            }
            return stationFree;
        }
    }
}
