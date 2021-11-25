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
        void UpdateStation(Station station);
        void UpdateStationName(Station station);
        void UpdateDrone(Drone drone);
        void UpdateDroneModel(Drone drone);
        void Updatecustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude);
        void UpdatecustomerNameAndPhone(Customer customer);
        void UpdateParcel(Parcel parcel);
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
