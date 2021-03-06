using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Runtime.CompilerServices;

namespace BlApi
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
        Parcel assignParcelToDrone(int droneId);
        Station displayStation(int Id);
        Drone displayDrone(int Id);
        Customer displayCustomer(int Id);
       Parcel displayParcel(int Id);
        IEnumerable<StationToList> displayStationList();
        IEnumerable<StationToList> displayFreeStationList();
        IEnumerable<DroneToList> displayDroneList();
        IEnumerable<DroneToList> displayDroneListFiltered(BO.WeightCategories? WC, BO.DroneStatus? DS);
        IEnumerable<CustomerToList> displayCustomerList();
        IEnumerable<ParcelToList> displayParcelList();
        IEnumerable<ParcelToList> displayParcelListFiltered(BO.WeightCategories? W, BO.Priorities? P, DateTime? D);
        IEnumerable<ParcelToList> displayFreeParcelList();
        IEnumerable<ParcelToList> displayParcelListFiltered(DateTime? date1, DateTime? date2);
        double DistanceTo(double lat1, double lon1, double lat2, double lon2);
        void ActSimulator(int DroneID, Action WPFUpdate, Func<bool> StopCheck);

        double[] electricityUse();
    }
}
