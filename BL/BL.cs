using IBL.BO;
using System;
using System.Collections.Generic;


namespace BL
{
    public class BL : IBL.IBL
    {

        IDAL.IDal dali = new DAL.DalObject.DalObject();
        public void Addcustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude)
        {
            IDAL.DO.Customer customer = new IDAL.DO.Customer { Id = customerId, Lattitude = customerLattitude, Longitude = customerLattitude, Name = customerName, Phone = customerPhone};
            dali.AddCustomer(customer);
        }

        public void AddDrone(int droneId, string droneModel, WeightCategories droneMaxWeight)
        {
            IDAL.DO.Drone drone = new IDAL.DO.Drone { Id = droneId, Model = droneModel, MaxWeight = (IDAL.DO.WeightCategories)droneMaxWeight };
            dali.AddDrone(drone);
        }

        public void AddParcel(int parcelId, int senderId, int targetId, WeightCategories parcelWeight, Priorities priority)
        {
            IDAL.DO.Parcel parcel = new IDAL.DO.Parcel { Id = parcelId, SenderId = senderId, TargetId = targetId, Weight = (IDAL.DO.WeightCategories)parcelWeight, Priority = (IDAL.DO.Priorities)priority };
            dali.AddParcel(parcel);
        }

        public void AddStation(int stationId, int stationName, double stationLongitude, double stationLattitude, int chargeSlots)
        {
            IDAL.DO.Station station = new IDAL.DO.Station { Id = stationId, Name = stationName, Longitude = stationLongitude, Lattitude = stationLattitude, ChargeSlots = chargeSlots };
            dali.AddStation(station);
            
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

        public IEnumerable<Customer> displaycustomerList()
        {
            throw new NotImplementedException();
        }

        public Drone displayDrone(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Drone> displayDroneList()
        {
            throw new NotImplementedException();
        }

        public Parcel displayParcel(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> displayParcelList()
        {
            throw new NotImplementedException();
        }

        public Station displayStation(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> displayStationList()
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
    }
}
