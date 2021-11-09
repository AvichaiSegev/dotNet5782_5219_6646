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
        void AddStation(int stationId, int stationName, double stationLongitude, double stationLattitude, int chargeSlots);
        void AddDrone(int droneId, string droneModel, WeightCategories droneMaxWeight);
        void Addcustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude);
        void AddParcel(int parcelId, int senderId, int targetId, WeightCategories parcelWeight, Priorities priority);

        //Update functions
        void UpdateStation(int stationId, int stationName, double stationLongitude, double stationLattitude, int chargeSlots);
        void UpdateStationName(int stationId, int stationName);
        void UpdateDrone(int droneId, string droneModel, WeightCategories droneMaxWeight);
        void UpdateDroneModel(int droneId, string droneModel);
        void Updatecustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude);
        void UpdatecustomerNameAndPhone(int customerId, string customerName, string customerPhone);
        void UpdateParcel(int parcelId, int senderId, int targetId, WeightCategories parcelWeight, Priorities priority);
        Station displayStation(int Id);
        Drone displayDrone(int Id);
        Customer displaycustomer(int Id);
       Parcel displayParcel(int Id);
        IEnumerable<Station> displayStationList();
        IEnumerable<Drone> displayDroneList();
        IEnumerable<Customer> displaycustomerList();
        IEnumerable<Parcel> displayParcelList();
        
        
        double[] electricityUse();
    }
}
