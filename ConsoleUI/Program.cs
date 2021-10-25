using System;
using IDAL.DO;
using DAL.DalObject;

namespace ConsoleUI
{
    public class Program
    {
        //function for
        static int options(string text)
        {
            int Choice;
            Console.WriteLine(text);
            int.TryParse(Console.ReadLine(), out Choice);
            return Choice;
        }
        public static void Main(string[] args)
        {
            int firstChoice = 0, secondChoice = 0;
            int stationId, droneId, costumerId, parcelId;
            int stationName = 0, chargeSlots = 0;
            string droneStatusString = "", droneMaxWeightString = "", costumerName = "", costumerPhone = "";
            double stationLongitude = 0, stationLattitude = 0, droneBattery = 0, costumerLongitude = 0, costumerLattitude = 0;
            string droneModel = "";
            DroneStatuses droneStatus = DroneStatuses.available;
            WeightCategories droneMaxWeight = WeightCategories.light;

            Console.WriteLine("Welcome to our program!\n");
            while (firstChoice != 5)
            {
                stationId = 0;
                droneId = 0;
                costumerId = 0;
                parcelId = 0;
                droneId = 0;
                stationName = 0;
                chargeSlots = 0;
                stationLongitude = 0;
                stationLattitude = 0;
                costumerLongitude = 0;
                costumerLattitude = 0;
                droneBattery = 0;
                droneStatus = DroneStatuses.available;
                droneStatusString = "";
                droneMaxWeight = WeightCategories.light;
                droneMaxWeightString = "";
                droneModel = "";
                costumerName = "";
                costumerPhone = "";
                firstChoice = options("~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~ Choose one of the follow options: ~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~\n" +
                    "1. Insert options.\n" +
                    "2. Update options.\n" +
                    "3. Display options.\n" +
                    "4. List view options.\n" +
                    "5. Exit.");
                switch (firstChoice)
                {
                    case 1:
                        secondChoice = options("=====Insert options=====\n" +
                            "1. Add a base station to the list of stations.\n" +
                            "2. Add a drone to the list of existing drones.\n" +
                            "3. Addition of a new customer to the customer list.\n" +
                            "4. Receipt of package for shipment.");
                        switch (secondChoice)
                        {
                            case 1:
                                Console.WriteLine("Enter station Id: ");
                                int.TryParse(Console.ReadLine(), out stationId);
                                Console.WriteLine("Enter station name: ");
                                int.TryParse(Console.ReadLine(), out stationName);
                                Console.WriteLine("Enter station longitude: ");
                                double.TryParse(Console.ReadLine(), out stationLongitude);
                                Console.WriteLine("Enter station lattitude: ");
                                double.TryParse(Console.ReadLine(), out stationLattitude);
                                Console.WriteLine("Enter station available charge slots: ");
                                int.TryParse(Console.ReadLine(), out chargeSlots);
                                DAL.DalObject.DalObject.AddStation(stationId, stationName, stationLongitude, stationLattitude, chargeSlots);
                                break;
                            case 2:
                                Console.WriteLine("Enter drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                Console.WriteLine("Enter drone model: ");
                                droneModel = Console.ReadLine();
                                Console.WriteLine("Enter max weight for the drone: ");
                                droneMaxWeightString = Console.ReadLine();
                                Console.WriteLine("Enter drone status: ");
                                droneStatusString = Console.ReadLine();
                                Console.WriteLine("Enter battery status for the drone: ");
                                double.TryParse(Console.ReadLine(), out droneBattery);
                                switch (droneStatusString)
                                {
                                    case "available":droneStatus = DroneStatuses.available; break;
                                    case "maintenance":droneStatus = DroneStatuses.maintenance; break;
                                    case "shipment":droneStatus = DroneStatuses.shipment; break;
                                    default:break;
                                }
                                switch (droneMaxWeightString)
                                {
                                    case "light": droneMaxWeight = WeightCategories.light; break;
                                    case "liver": droneMaxWeight = WeightCategories.liver; break;
                                    case "medium": droneMaxWeight = WeightCategories.medium; break;
                                    default: break;
                                }
                                DAL.DalObject.DalObject.AddDrone(droneId, droneModel, droneMaxWeight, droneStatus, droneBattery);
                                break;
                            case 3:
                                Console.WriteLine("Enter costumer Id: ");
                                int.TryParse(Console.ReadLine(), out costumerId);
                                Console.WriteLine("Enter costumer name: ");
                                costumerName = Console.ReadLine();
                                Console.WriteLine("Enter costumer phone: ");
                                costumerPhone = Console.ReadLine();
                                Console.WriteLine("Enter costumer longitude: ");
                                double.TryParse(Console.ReadLine(), out costumerLongitude);
                                Console.WriteLine("Enter costumer lattitude: ");
                                double.TryParse(Console.ReadLine(), out costumerLattitude);
                                DAL.DalObject.DalObject.AddCostumer(costumerId, costumerName, costumerPhone, costumerLongitude, costumerLattitude);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        secondChoice = options("=====Update options=====\n" +
                            "1. Assign a package to a drone.\n" +
                            "2. Collection of a package by drone.\n" +
                            "3. Delivery package to customer.\n" +
                            "4. Sending a drone for charging at a base station.\n" +
                            "5. Release drone from charging at base station.");
                        break;
                    case 3:
                        secondChoice = options("=====Display options=====\n" +
                            "1. Base station view.\n" +
                            "2. Drone view.\n" +
                            "3. Customer view.\n" +
                            "4. Package view.");
                        switch (secondChoice)
                        {
                            case 1:
                                Console.WriteLine("Enter the Id of the station:");
                                int.TryParse(Console.ReadLine(), out stationId);
                                Station station = DAL.DalObject.DalObject.displayStation(stationId);
                                Console.WriteLine(station.ToString());
                                break;
                            case 2:
                                Console.WriteLine("Enter the Id of the drone:");
                                int.TryParse(Console.ReadLine(), out droneId);
                                Drone drone = DAL.DalObject.DalObject.displayDrone(droneId);
                                Console.WriteLine(drone.ToString());
                                break;
                            case 3:
                                Console.WriteLine("Enter the Id of the costumer:");
                                int.TryParse(Console.ReadLine(), out costumerId);
                                Costumer costumer = DAL.DalObject.DalObject.displayCostumer(costumerId);
                                Console.WriteLine(costumer.ToString());
                                break;
                            case 4:
                                Console.WriteLine("Enter the Id of the parcel:");
                                int.TryParse(Console.ReadLine(), out parcelId);
                                Parcel parcel = DAL.DalObject.DalObject.displayParcel(parcelId);
                                Console.WriteLine(parcel.ToString());
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4:
                        secondChoice = options("=====List view options=====\n" +
                        "1. Displays a list of base stations.\n" +
                        "2. Displays the list of drones.\n" +
                        "3. View customer list.\n" +
                        "4. Displays the list of packages.\n" +
                        "5. Displays a list of packages not yet associated with a drone.\n" +
                        "6. Display base stations with available charging stations.");    
                        switch (secondChoice)
                        {
                            case 1:
                                foreach (Station item in DAL.DalObject.DalObject.displayStationList()){ Console.WriteLine(item.ToString()); }
                                break;
                            case 2:
                                foreach (Drone item in DAL.DalObject.DalObject.displayDroneList()) { Console.WriteLine(item.ToString()); }
                                break;
                            case 3:
                                foreach (Costumer item in DAL.DalObject.DalObject.displayCostumerList()) { Console.WriteLine(item.ToString()); }
                                break;
                            case 4:
                                foreach (Parcel item in DAL.DalObject.DalObject.displayParcelList()) { Console.WriteLine(item.ToString()); }
                                break;
                            case 5:
                                foreach(Parcel item in DAL.DalObject.DalObject.displayParcelList()){ if (item.DroneId == -1) { Console.WriteLine(item.ToString()); } }
                                break;
                            case 6:
                                foreach (Station item in DAL.DalObject.DalObject.displayStationList()) { if (item.ChargeSlots > 0) { Console.WriteLine(item.ToString()); } }
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
