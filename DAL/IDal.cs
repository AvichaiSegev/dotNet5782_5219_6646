using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        Station displayStation(int Id);
        Drone displayDrone(int Id);
        Customer displayCustomer(int Id);
        Parcel displayParcel(int Id);
        IEnumerable<Station> displayStationList();
        IEnumerable<Drone> displayDroneList();
        IEnumerable<Customer> displayCustomerList();
        IEnumerable<Parcel> displayParcelList();
        void AddStation(Station station);
        void AddDrone(Drone drone);
        void AddCustomer(Customer customer);
        void AddParcel(Parcel parcel);
        void UpdateStation(Station station);
        void UpdateDrone(Drone drone);
        void UpdateCustomer(Customer customer);
        void UpdateParcel(Parcel parcel);
        double[] electricityUse();

    }
}
