using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IBL
    {
        //Add functions
        void AddStation(Station station);
        void AddDrone(Drone drone, int stationId);
        void Addcustomer(Customer customer);
        void AddParcel(Parcel parcel, int senderId, int gettedId);

        //Update functions
        void UpdateStation(int stationId, int stationName, double stationLongitude, double stationLattitude, int chargeSlots);
        void UpdateStationName(int stationId, int stationName);
        void UpdateDrone(int droneId, string droneModel, WeightCategories droneMaxWeight);
        void UpdateDroneModel(int droneId, string droneModel);
        void Updatecustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude);
        void UpdatecustomerNameAndPhone(int customerId, string customerName, string customerPhone);
        void UpdateParcel(int parcelId, int senderId, int targetId, WeightCategories parcelWeight, Priorities priority);
        void sendDroneToCharging(int droneId);
        void releaseDroneFromCharging(int droneId, double chargingTime);
        void collectParcelByDrone(int droneId);
        void provideParcelByDrone(int droneId);
        void assignParcelToDrone(int droneId);
        Station displayStation(int Id);
        Drone displayDrone(int Id);
        Customer displaycustomer(int Id);
       Parcel displayParcel(int Id);
        IEnumerable<StationToList> displayStationList();
        IEnumerable<StationToList> displayFreeStationList();
        IEnumerable<DroneToList> displayDroneList();
        IEnumerable<CustomerToList> displaycustomerList();
        IEnumerable<ParcelToList> displayParcelList();
        IEnumerable<ParcelToList> displayFreeParcelList();


        double[] electricityUse();
    }
}
