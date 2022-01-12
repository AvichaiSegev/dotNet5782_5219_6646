using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.CompilerServices;
using BlApi;

namespace BL
{
    public class BL : BlApi.IBL
    {
        List<DroneToList> droneList = new List<DroneToList>();
        double electricityUseForVacantDrone, electricityUseForLightParcel, electricityUseForMediumParcel, electricityUseForHeavyParcel, chargingRate;
        DalApi.IDal dali;
        static readonly Random randy = new Random();
        internal BL()//constractor
        {
            dali = DalApi.DalFactory.GetDal();
            double[] electricity = dali.electricityUse();
            electricityUseForVacantDrone = electricity[0]; electricityUseForLightParcel = electricity[1]; electricityUseForMediumParcel = electricity[2]; electricityUseForHeavyParcel = electricity[3]; chargingRate = electricity[4];
            foreach (var drone in dali.displayDroneList())//Update drone list from the data base
            {
                DroneToList drone1 = new DroneToList();
                drone1.id = drone.Id;
                drone1.model = drone.Model;
                drone1.maxWeight = (WeightCategories)drone.MaxWeight;
                DO.Parcel parcelDidNotDelivered = new DO.Parcel();
                parcelDidNotDelivered.Id = -1;
                foreach (var parcel in dali.displayParcelList()) {
                    if (parcel.DroneId == drone.Id && parcel.Provided == null) {
                        parcelDidNotDelivered = parcel;
                        break;
                    }
                }
                if (parcelDidNotDelivered.Id != -1) {//If suitable drone found
                    drone1.status = DroneStatus.delivery;
                    if (parcelDidNotDelivered.Assigned != null && parcelDidNotDelivered.Collected == null) { drone1.location = nearStation(dali.displayCustomer(parcelDidNotDelivered.SenderId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.SenderId).Longitude); }
                    if (parcelDidNotDelivered.Collected != null && parcelDidNotDelivered.Provided == null) { drone1.location = new Location(dali.displayCustomer(parcelDidNotDelivered.SenderId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.SenderId).Longitude); }
                    double minBattery;
                    Location targetnearStation = nearStation(dali.displayCustomer(parcelDidNotDelivered.TargetId).Lattitude, dali.displayCustomer(parcelDidNotDelivered.TargetId).Longitude);
                    DO.Customer customerTarget = dali.displayCustomer(parcelDidNotDelivered.TargetId);
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
                    drone1.battery = randy.Next((int)minBattery, 100);
                    drone1.parcelNumber = parcelDidNotDelivered.Id;
                }
                else {
                    int booly = randy.Next(1, 2);
                    if (booly == 1) { drone1.status = DroneStatus.free; }
                    if (booly == 2) { drone1.status = DroneStatus.matance; }
                }
                if (drone1.status == DroneStatus.matance)
                {
                    int numStation = randy.Next(1, stationsAmount());
                    int i = 0;
                    foreach (var station in dali.displayStationList()) { if (i == numStation) { drone1.location = new Location(station.Longitude, station.Lattitude); i++; } }//random location for drone1
                    drone1.battery = randy.Next(0, 20);
                }
                else if (drone1.status == DroneStatus.free)
                {
                    List<DO.Parcel> parcelDelivered = listParcelDelivered();
                    int parcelsDelivered = randy.Next(0, parcelDelivered.Count);
                    DO.Customer targetCutomer = new DO.Customer();
                    if (parcelDelivered.Count != 0) { targetCutomer = new DO.Customer(dali.displayCustomer(parcelDelivered[parcelsDelivered].TargetId)); }
                    if (parcelDelivered.Count == 0) { targetCutomer = new DO.Customer(); }
                    drone1.location = new Location(targetCutomer.Lattitude, targetCutomer.Longitude);
                    Location targetnearStation = nearStation(targetCutomer.Lattitude, targetCutomer.Longitude);
                    double destinationToNearStation = DistanceTo(drone1.location.latitude, drone1.location.longitude, targetCutomer.Lattitude, targetCutomer.Longitude);
                    drone1.battery = 100 - randy.NextDouble() * (int)electricityUseForVacantDrone * destinationToNearStation;
                }
                droneList.Add(drone1);

            }
        }

        static BL() { }
        private static BL instance;
        static readonly object lockname = new object();

        public static BL Instace
        {
            get
            {
                if (instance == null)
                {
                    lock (lockname)
                    {
                        if (instance == null)
                        {
                            instance = new BL();// Explanation required////////////
                        }
                    }
                }
                return instance;
            }
        }
        //Add functions
        
        void Addcustomer(Customer customer)//Add customer
        {
            DO.Customer DCustomer = new DO.Customer { Id = customer.id, Lattitude = customer.location.latitude, Longitude = customer.location.longitude, Name = customer.name, Phone = customer.phone };
            dali.AddCustomer(DCustomer);
        }

        
        public void AddDrone(Drone drone, int stationId)//Add drone
        {
            try
            {
                double DBattery = 20 + 20 * randy.NextDouble();
                Location Dlocation = new Location(dali.displayStation(stationId).Longitude, dali.displayStation(stationId).Lattitude);

                droneList.Add(new DroneToList() { id = drone.id, maxWeight = drone.maxWeight, battery = DBattery, location = Dlocation, status = DroneStatus.matance, model = drone.model });
                DO.Drone Ddrone = new DO.Drone { Id = drone.id, MaxWeight = (DO.WeightCategories)drone.maxWeight, Model = drone.model };
                dali.AddDrone(Ddrone);
            }
            catch (DO.IdDoesNotExistException err)
            {
                throw new IdDoesNotExist(err.ID);
            }
        }


        
        public void AddParcel(Parcel parcel, int senderId, int gettedId)//Add parcel
        {
            DO.Parcel DParcel = new DO.Parcel { Id = parcel.Id, SenderId = senderId, TargetId = gettedId, Weight = (DO.WeightCategories)parcel.weight, Priority = (DO.Priorities)parcel.priority, DroneId = int.MinValue, Defined = DateTime.Now, Assigned = null, Collected = null, Provided = null };
            dali.AddParcel(DParcel);
        }

        
        public void AddStation(Station station)//Add station
        {
            DO.Station Dstation = new DO.Station { Id = station.id, Name = station.name, Longitude = station.location.longitude, Lattitude = station.location.latitude, freeChargeSlots = station.numFreeChargingStands };
            dali.AddStation(Dstation);

        }
        //Update functions
        
        public void Updatecustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude)//Update customer
        {
            DO.Customer customer = new DO.Customer { Id = customerId, Name = customerName, Phone = customerPhone, Lattitude = customerLattitude, Longitude = customerLongitude };
            dali.UpdateCustomer(customer);
        }
        
        public void UpdatecustomerNameAndPhone(Customer customer)//Update customer name and phone
        {
            DO.Customer Dcustomer = dali.displayCustomer(customer.id);
            Dcustomer.Name = customer.name;
            Dcustomer.Phone = customer.phone;
            dali.UpdateCustomer(Dcustomer);
        }

        
        public void UpdateDrone(Drone drone)//Update drone
        {
            DroneToList drone1;
            if (drone.parcel == null)
            {
                drone1 = new DroneToList() { id = drone.id, battery = drone.battery, model = drone.model, status = drone.status, parcelNumber = int.MinValue, location = drone.location, maxWeight = drone.maxWeight };
            }
            else
                drone1 = new DroneToList() { id = drone.id, battery = drone.battery, model = drone.model, status = drone.status, parcelNumber = drone.parcel.id, location = drone.location, maxWeight = drone.maxWeight };
            //droneList.Add(drone1);
            if (!droneList.Any(x => x.id == drone.id))
            {
                throw new IdDoesNotExist(drone.id);
            }
            int i = 0;
            while (droneList[i].id != drone.id)
            {
                i++;
            }
            droneList[i] = drone1;
            DO.Drone Ddrone = new DO.Drone() { Id = drone.id, MaxWeight = (DO.WeightCategories)drone.maxWeight, Model = drone.model };
            dali.UpdateDrone(Ddrone);
        }

        
        public void UpdateDroneModel(Drone drone)//Update drone model
        {
            DroneToList drone1 = displayDroneToList(drone.id);
            drone.location = drone1.location;
            drone.maxWeight = drone1.maxWeight;
            drone.parcel = new ParcelInTransfer() { id = drone1.parcelNumber };
            drone.status = drone1.status;
            drone.battery = drone1.battery;
            UpdateDrone(drone);

        }

        
        public void UpdateParcel(Parcel parcel)//Update parcel
        {
            DO.Parcel Dparcel = dali.displayParcel(parcel.Id);
            Dparcel.Priority = (DO.Priorities)parcel.priority;
            Dparcel.Provided = parcel.providedParcelTime;
            Dparcel.SenderId = parcel.delivered.id;
            Dparcel.TargetId = parcel.getted.id;
            Dparcel.Weight = (DO.WeightCategories)parcel.weight;
            Dparcel.DroneId = parcel.droneInParcel.id;
            Dparcel.Defined = parcel.definedParcelTime;
            Dparcel.Collected = parcel.collectedParcelTime;
            Dparcel.Assigned = parcel.assignedParcelTime;
            dali.UpdateParcel(Dparcel);
        }

        
        public void UpdateStation(Station station)//Update station
        {
            DO.Station Dstation = dali.displayStation(station.id);
            Dstation.freeChargeSlots = station.numFreeChargingStands;
            Dstation.Lattitude = station.location.latitude;
            Dstation.Longitude = station.location.longitude;
            Dstation.Name = station.name;
            dali.UpdateStation(Dstation);
        }

        
        public void UpdateStationName(Station station)//Update station name
        {
            DO.Station Dstation = dali.displayStation(station.id);
            Dstation.Name = station.name;
            dali.UpdateStation(Dstation);
        }
        
        public void assignParcelToDrone(int droneId)//Assign parcel to drone
        {
            Drone drone = displayDrone(droneId);
            Parcel parcel = assignAParcelToDrone(drone, Priorities.emergency);
            if (parcel.Id == -1)
            {
                parcel = assignAParcelToDrone(drone, Priorities.quick);
                if (parcel.Id == -1)
                {
                    parcel = assignAParcelToDrone(drone, Priorities.regular);
                    if (parcel.Id == -1) { throw new NoSuiTablePackageFound(); }
                }
            }
            UpdateDrone(new Drone() { id = drone.id, battery = drone.battery, location = drone.location, maxWeight = drone.maxWeight, model = drone.model, status = DroneStatus.delivery, parcel = new ParcelInTransfer() { id = parcel.Id, getter = parcel.getted, sender = parcel.delivered, collection = displayCustomer(parcel.delivered.id).location, target = displayCustomer(parcel.getted.id).location, priority = parcel.priority, status = false, weight = parcel.weight } });
            parcel.droneInParcel = new DroneInParcel() { battery = drone.battery, id = drone.id, location = drone.location };
            parcel.assignedParcelTime = DateTime.Now;
            UpdateParcel(parcel);
        }
        Parcel assignAParcelToDrone(Drone drone, Priorities priority)//Assign parcel to drone - Internal function
        {
            Parcel Bparcel = new Parcel() { Id = -1 };
            double closestDistance = double.MaxValue;
            foreach (var parcel in dali.displayParcelList())
            {
                if (parcel.Weight <= (DO.WeightCategories)drone.maxWeight)
                {
                    Location senderL = new Location(dali.displayCustomer(parcel.SenderId).Longitude, dali.displayCustomer(parcel.SenderId).Lattitude);
                    Location targetL = new Location(dali.displayCustomer(parcel.TargetId).Longitude, dali.displayCustomer(parcel.TargetId).Lattitude);
                    Location nearTargetStation = nearStation(targetL.latitude, targetL.longitude);
                    double distanceBetweenCustomers = DistanceTo(senderL.latitude, senderL.longitude, targetL.latitude, targetL.longitude);
                    double totalDistance = DistanceTo(drone.location.latitude, drone.location.longitude, senderL.latitude, senderL.longitude) + distanceBetweenCustomers + DistanceTo(targetL.latitude, targetL.longitude, nearTargetStation.latitude, nearTargetStation.longitude);
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
                        if (distanceToParcel < closestDistance)//Calculating closest parcel
                        {
                            Bparcel.Id = parcel.Id;
                            Bparcel.priority = (Priorities)parcel.Priority;
                            Bparcel.providedParcelTime = parcel.Provided;
                            Bparcel.delivered = new CustomerInParcel() { id = parcel.SenderId };
                            Bparcel.getted = new CustomerInParcel() { id = parcel.TargetId };
                            Bparcel.weight = (WeightCategories)parcel.Weight;
                            Bparcel.droneInParcel = new DroneInParcel() { id = parcel.DroneId, battery = drone.battery, location = drone.location };
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

        
        public void collectParcelByDrone(int droneId)//Collect parcel by drone
        {
            DroneToList droneToList = displayDroneToList(droneId);
            Parcel parcel = displayParcel(droneToList.parcelNumber);
            Customer customer = displayCustomer(parcel.delivered.id);
            if (droneToList.status != DroneStatus.delivery || parcel.droneInParcel.id != droneId || parcel.assignedParcelTime == null || parcel.collectedParcelTime != null)
            {
                throw new DroneDoesNotSuitable();
            }
            double distance = DistanceTo(droneToList.location.latitude, droneToList.location.longitude, customer.location.latitude, customer.location.longitude);
            droneToList.battery = droneToList.battery - electricityUseForVacantDrone * distance;
            droneToList.location = customer.location;
            UpdateDrone(new Drone() { battery = droneToList.battery, id = droneToList.id, location = droneToList.location, maxWeight = (WeightCategories)droneToList.maxWeight, model = droneToList.model, parcel = new ParcelInTransfer() { id = droneToList.parcelNumber }, status = droneToList.status });
            parcel.collectedParcelTime = DateTime.Now;
            UpdateParcel(parcel);
        }


        
        public void provideParcelByDrone(int droneId)//Provide parcel by drone
        {
            DroneToList droneToList = displayDroneToList(droneId);
            Parcel parcel = displayParcel(droneToList.parcelNumber);
            Customer customer = displayCustomer(parcel.getted.id);
            if (droneToList.status != DroneStatus.delivery || parcel.droneInParcel.id != droneId || parcel.collectedParcelTime == null || parcel.providedParcelTime != null)
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
            UpdateDrone(new Drone() { battery = droneToList.battery, id = droneToList.id, location = droneToList.location, maxWeight = (WeightCategories)droneToList.maxWeight, model = droneToList.model, parcel = new ParcelInTransfer(), status = DroneStatus.free });
            parcel.providedParcelTime = DateTime.Now;
            UpdateParcel(parcel);
        }

        
        public void sendDroneToCharging(int droneId)//Send drone to charging
        {
            if (!droneList.Any(x => x.id == droneId))
            {
                throw new IdDoesNotExist(droneId);
            }
            DroneToList drone = displayDroneToList(droneId);
            if (drone.status != DroneStatus.free) { throw new dronesStatusIsNotApplicable(); }
            Location nearstation = nearStation(drone.location.latitude, drone.location.longitude);
            double distance = DistanceTo(drone.location.latitude, drone.location.longitude, nearstation.latitude, nearstation.longitude);
            double distanceAbility = drone.battery / electricityUseForVacantDrone;
            if (distanceAbility < distance) { throw new dontHaveMuchBattery(); }
            drone.battery -= (distance * electricityUseForVacantDrone);
            drone.location = nearstation;
            drone.status = DroneStatus.matance;
            UpdateDrone(new Drone() { id = drone.id, battery = drone.battery, location = drone.location, maxWeight = drone.maxWeight, model = drone.model, status = DroneStatus.matance });
            DO.Station station = dali.displayStationByLocation(nearstation.latitude, nearstation.longitude);
            UpdateStation(new Station() { id = station.Id, name = station.Name, location = new Location(station.Longitude, station.Lattitude), numFreeChargingStands = station.freeChargeSlots - 1 });
            dali.AddDroneCharge(new DO.DroneCharge() { droneId = drone.id, StationId = station.Id });
        }

        
        public void releaseDroneFromCharging(int droneId, double chargingTime)//Release drone from charging
        {
            if (!droneList.Any(x => x.id == droneId))
            {
                throw new IdDoesNotExist(droneId);
            }
            if (displayDroneToList(droneId).status != DroneStatus.matance) { throw new dronesStatusIsNotApplicable(); }
            Drone drone = displayDrone(droneId);
            double extraBattery = chargingTime * chargingRate;
            if (drone.battery + extraBattery > 100) { extraBattery = 100 - drone.battery; }
            UpdateDrone(new Drone() { id = drone.id, battery = drone.battery + extraBattery, location = drone.location, maxWeight = drone.maxWeight, model = drone.model, status = DroneStatus.free });
            DO.Station station = dali.displayStationByLocation(drone.location.latitude, drone.location.longitude);
            UpdateStation(new Station() { id = station.Id, name = station.Name, location = new Location(station.Longitude, station.Lattitude), numFreeChargingStands = station.freeChargeSlots + 1 });
            dali.deleteDroneCharge(droneId);
        }
        //Display functions
        public Customer displayCustomer(int Id)//display customer
        {
            DO.Customer customer1 = dali.displayCustomer(Id);
            Customer customer2 = new Customer();
            customer2.id = customer1.Id;
            customer2.name = customer1.Name;
            customer2.phone = customer1.Phone;
            customer2.location = new Location(customer1.Longitude, customer1.Lattitude);
            return customer2;
        }
        
        public IEnumerable<CustomerToList> displayCustomerList()//display customer list
        {
            List<CustomerToList> list1 = new List<CustomerToList>();

            foreach (var item in dali.displayCustomerList()) {
                int senderedButDidntProvided = 0, SenderedAndProvided = 0, onTheWay = 0, getted = 0;
                foreach (var item1 in dali.displayParcelList())
                {
                    if (item1.SenderId == item.Id && item1.Collected != null && item1.Provided == null) { senderedButDidntProvided += 1; }
                    if (item1.SenderId == item.Id && item1.Provided != null) { senderedButDidntProvided += 1; }
                    if (item1.TargetId == item.Id && item1.Provided != null) { getted += 1; }
                    if (item1.TargetId == item.Id && item1.Provided == null) { onTheWay += 1; }
                }
                list1.Add(new CustomerToList() { id = item.Id, name = item.Name, phone = item.Phone, parcelsWasSendedButDidntProvidedYet = senderedButDidntProvided, parcelsWasSendedAndprovided = SenderedAndProvided, parcelOnTheWay = onTheWay, parcelsGetted = getted });

            }
            return list1;
        }
        
        public Station displayStation(int Id)//display station
        {
            DO.Station station1 = dali.displayStation(Id);
            Station station2 = new Station();
            station2.id = station1.Id;
            station2.location = new Location(station1.Longitude, station1.Lattitude);
            station2.name = station1.Name;
            station2.numFreeChargingStands = station1.freeChargeSlots;
            return station2;
        }
        
        public Drone displayDrone(int Id)//display drone
        {
            if (!droneList.Any(x => x.id == Id))
            {
                throw new IdDoesNotExist(Id);
            }
            DroneToList drone1 = new DroneToList();
            foreach (var drone in droneList) { if (drone.id == Id) { drone1 = drone; break; } }
            Drone drone2 = new Drone() { id = drone1.id, battery = drone1.battery, location = drone1.location, maxWeight = drone1.maxWeight, model = drone1.model, parcel = new ParcelInTransfer() { id = drone1.parcelNumber }, status = drone1.status };
            return drone2;
        }
        
        public Parcel displayParcel(int Id)//display parcel
        {
            DO.Parcel parcel1 = dali.displayParcel(Id);
            Parcel parcel2 = new Parcel();
            parcel2.Id = parcel1.Id;
            switch ((WeightCategories)parcel1.Weight)
            {
                case WeightCategories.light:
                    parcel1.Weight = DO.WeightCategories.light;
                    break;
                case WeightCategories.medium:
                    parcel1.Weight = DO.WeightCategories.medium;
                    break;
                case WeightCategories.liver:
                    parcel1.Weight = DO.WeightCategories.liver;
                    break;
                default: break;
            }
            switch ((Priorities)parcel1.Priority)
            {
                case Priorities.emergency:
                    parcel1.Priority = DO.Priorities.emergency;
                    break;
                case Priorities.quick:
                    parcel1.Priority = DO.Priorities.quick;
                    break;
                case Priorities.regular:
                    parcel1.Priority = DO.Priorities.regular;
                    break;
                default: break;
            }
            parcel2.assignedParcelTime = parcel1.Assigned;
            parcel2.collectedParcelTime = parcel1.Collected;
            parcel2.definedParcelTime = parcel1.Defined;
            parcel2.providedParcelTime = parcel1.Provided;
            Customer customer = displayCustomer(parcel1.TargetId);
            parcel2.getted = new CustomerInParcel() { id = customer.id, name = customer.name };
            customer = displayCustomer(parcel1.SenderId);
            parcel2.delivered = new CustomerInParcel() { id = customer.id, name = customer.name };
            if (parcel1.DroneId != int.MinValue)
            {
                Drone drone = displayDrone(parcel1.DroneId);
                parcel2.droneInParcel = new DroneInParcel() { id = drone.id, battery = drone.battery, location = drone.location };
            }
            else
            {
                parcel2.droneInParcel = new DroneInParcel() { id = int.MinValue };
            }
            return parcel2;
        }
        DroneToList displayDroneToList(int droneId)//Display drone to list
        {
            if (!droneList.Any(x => x.id == droneId))
            {
                throw new IdDoesNotExist(droneId);
            }
            return droneList.Find(x => x.id == droneId);
        }
        //Display list functions
        
        public IEnumerable<DroneToList> displayDroneList()//display drone list
        {
            return droneList;
        }

        
        public IEnumerable<DroneToList> displayDroneListFiltered(BO.WeightCategories? WC, BO.DroneStatus? DS)
        {
            return droneList.FindAll(x => (WC is null || x.maxWeight == WC) && (DS is null || x.status == DS));
        }
        
        public IEnumerable<ParcelToList> displayParcelListFiltered(BO.WeightCategories? W, BO.Priorities? P, DateTime? D)
        {
            IEnumerable<ParcelToList> parcelList = displayParcelList();
            return parcelList.Where(x => (W is null || x.weight == W) && (P is null || x.priority == P));
        }
        
        public IEnumerable<ParcelToList> displayParcelListFiltered(DateTime? date1, DateTime? date2)
        {
            List<ParcelToList> list1 = new List<ParcelToList>();
            foreach (var item in dali.displayParcelList(x => ((x.Defined >= date1 && x.Defined <= date2))))
            {
                ParcelToList parcel = new ParcelToList();
                parcel.parcelId = item.Id;
                parcel.deliveredCustomerName = displayCustomer(item.SenderId).name;
                parcel.gettedCustomerName = displayCustomer(item.TargetId).name;
                switch ((WeightCategories)item.Weight)
                {
                    case WeightCategories.light:
                        parcel.weight = BO.WeightCategories.light;
                        break;
                    case WeightCategories.medium:
                        parcel.weight = BO.WeightCategories.medium;
                        break;
                    case WeightCategories.liver:
                        parcel.weight = BO.WeightCategories.liver;
                        break;
                    default: break;
                }
                switch ((Priorities)item.Priority)
                {
                    case Priorities.emergency:
                        parcel.priority = BO.Priorities.emergency;
                        break;
                    case Priorities.quick:
                        parcel.priority = BO.Priorities.quick;
                        break;
                    case Priorities.regular:
                        parcel.priority = BO.Priorities.regular;
                        break;
                    default: break;
                }
                list1.Add(parcel);
            }
            return list1;
        }
        
        public IEnumerable<ParcelToList> displayParcelList()//display parcel
        {
            List<ParcelToList> list1 = new List<ParcelToList>();
            foreach (var item in dali.displayParcelList())
            {
                ParcelToList parcel = new ParcelToList();
                parcel.parcelId = item.Id;
                parcel.deliveredCustomerName = displayCustomer(item.SenderId).name;
                parcel.gettedCustomerName = displayCustomer(item.TargetId).name;
                switch ((WeightCategories)item.Weight)
                {
                    case WeightCategories.light:
                        parcel.weight = BO.WeightCategories.light;
                        break;
                    case WeightCategories.medium:
                        parcel.weight = BO.WeightCategories.medium;
                        break;
                    case WeightCategories.liver:
                        parcel.weight = BO.WeightCategories.liver;
                        break;
                    default: break;
                }
                switch ((Priorities)item.Priority)
                {
                    case Priorities.emergency:
                        parcel.priority = BO.Priorities.emergency;
                        break;
                    case Priorities.quick:
                        parcel.priority = BO.Priorities.quick;
                        break;
                    case Priorities.regular:
                        parcel.priority = BO.Priorities.regular;
                        break;
                    default: break;
                }
                list1.Add(parcel);
            }
            return list1;
        }


        
        public IEnumerable<StationToList> displayStationList()//dispaly station list
        {
            List<StationToList> list1 = new List<StationToList>();
            foreach (var item in dali.displayStationList())
            {
                StationToList station = new StationToList();
                station.id = item.Id;
                station.name += item.Name;
                station.numFreeChargingStands = item.freeChargeSlots;
                list1.Add(station);
            }
            return list1;
        }

        
        public double[] electricityUse()
        {
            throw new NotImplementedException();
        }



        
        public IEnumerable<ParcelToList> displayFreeParcelList()//display free parcel list
        {
            List<ParcelToList> list1 = new List<ParcelToList>();
            IEnumerable<DO.Parcel> list2 = dali.displayParcelList();
            ParcelToList parcel = new ParcelToList();
            foreach (var item in list2)
            {
                if (item.Assigned == null)
                {
                    parcel.parcelId = item.Id;
                    switch ((WeightCategories)item.Weight)
                    {
                        case WeightCategories.light:
                            parcel.weight = BO.WeightCategories.light;
                            break;
                        case WeightCategories.medium:
                            parcel.weight = BO.WeightCategories.medium;
                            break;
                        case WeightCategories.liver:
                            parcel.weight = BO.WeightCategories.liver;
                            break;
                        default: break;
                    }
                    switch ((Priorities)item.Priority)
                    {
                        case Priorities.emergency:
                            parcel.priority = BO.Priorities.emergency;
                            break;
                        case Priorities.quick:
                            parcel.priority = BO.Priorities.quick;
                            break;
                        case Priorities.regular:
                            parcel.priority = BO.Priorities.regular;
                            break;
                        default: break;
                    }
                    list1.Add(parcel);
                }
            }
            return list1;
        }
        
        public IEnumerable<StationToList> displayFreeStationList()//Display free station list
        {
            List<StationToList> list1 = new List<StationToList>();
            foreach (var item in dali.displayStationList())
            {
                if (item.freeChargeSlots > 0)
                {
                    StationToList station = new StationToList();
                    station.id = item.Id;
                    station.name += item.Name;
                    station.numFreeChargingStands = item.freeChargeSlots;
                    list1.Add(station);
                }
            }
            return list1;
        }
        //Help mathods
        Location nearStation(double latitude, double longitude)//calculating where is the closest station
        {
            DO.Station nearStation = new DO.Station();
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
        double DistanceTo(double lat1, double lon1, double lat2, double lon2)//calculating distance between 2 locations
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

        List<DO.Parcel> listParcelDelivered()
        {
            List<DO.Parcel> parcelDelivered = new List<DO.Parcel>();
            foreach(var parcel in dali.displayParcelList())
            {
                if(parcel.Provided != null)
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

        List<DO.Station> listFreeStation()
        {
            List<DO.Station> stationFree = new List<DO.Station>();
            foreach(var station in dali.displayStationList())
            {
                if (station.freeChargeSlots != 0)
                {
                    stationFree.Add(station);
                }
            }
            return stationFree;
        }

        void IBL.Addcustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
        void ActSimulator(int DroneID, Action WPFUpdate, Func<bool> StopCheck)
        {

        }

    }
}
