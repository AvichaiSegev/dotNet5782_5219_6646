using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Runtime.CompilerServices;

namespace DalApi
{
    public interface IDal
    {
        Station displayStation(int Id);
        Drone displayDrone(int Id);
        Customer displayCustomer(int Id);
        Parcel displayParcel(int Id);
        Station displayStationByLocation(double latitude, double longitude);
        DroneCharge displayDroneCharge(int Id);
        IEnumerable<Station> displayStationList();
        IEnumerable<Drone> displayDroneList();
        IEnumerable<Customer> displayCustomerList();
        IEnumerable<Parcel> displayParcelList();
        IEnumerable<DroneCharge> displayDroneChargeList();
        void AddStation(Station station);
        void AddDrone(Drone drone);
        void AddCustomer(Customer customer);
        void AddParcel(Parcel parcel);
        void AddDroneCharge(DroneCharge droneCharge);
        void UpdateStation(Station station);
        void UpdateDrone(Drone drone);
        void UpdateCustomer(Customer customer);
        void UpdateParcel(Parcel parcel);
        void deleteDroneCharge(int Id);
        double[] electricityUse();

        IEnumerable<Parcel> displayParcelList(Predicate<Parcel> predicate);
    }
}
