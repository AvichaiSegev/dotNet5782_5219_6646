using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    interface IDal
    {
        Station displayStation(int Id);
        Drone displayDrone(int Id);
        customer displaycustomer(int Id);
        Parcel displayParcel(int Id);
        IEnumerable<Station> displayStationList();
        IEnumerable<Drone> displayDroneList();
        IEnumerable<customer> displaycustomerList();
        IEnumerable<Parcel> displayParcelList();
        void AddStation(int stationId, int stationName, double stationLongitude, double stationLattitude, int chargeSlots);
        void AddDrone(int droneId, string droneModel, WeightCategories droneMaxWeight);
        void Addcustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude);
        void AddParcel(int parcelId, int senderId, int targetId, WeightCategories parcelWeight, Priorities priority);
        void UpdateStation(int stationId, int stationName, double stationLongitude, double stationLattitude, int chargeSlots);
        void UpdateDrone(int droneId, string droneModel, WeightCategories droneMaxWeight);
        void Updatecustomer(int customerId, string customerName, string customerPhone, double customerLongitude, double customerLattitude);
        void UpdateParcel(int parcelId, int senderId, int targetId, WeightCategories parcelWeight, Priorities priority);
        double[] electricityUse();

    }
}
