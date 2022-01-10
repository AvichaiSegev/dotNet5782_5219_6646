/* ~ ~ ~ The main program ~ ~ ~ */
//By: Avichai Segev & Benaya Halevi.

using System;
using IDAL.DO;
using DAL.DalObject;

namespace ConsoleUI
{
    public class Program
    {
        //function for manager the choice of the user:
        static int options(string text)
        {
            int Choice;
            Console.WriteLine(text);
            int.TryParse(Console.ReadLine(), out Choice);
            return Choice;
        }
        public static void Main(string[] args)
        {
            //Decleration of variables for save (in the continue) the user input:
            int firstChoice = 0, secondChoice = 0;
            int stationId, droneId, customerId, parcelId, senderId = 0, targetId = 0;
            int stationName = 0, chargeSlots = 0;
            string droneMaxWeightString = "", customerName = "", customerPhone = "", parcelWeightString = "", priorityString = "";
            double stationLongitude = 0, stationLattitude = 0, customerLongitude = 0, customerLattitude = 0;
            string droneModel = "";
            WeightCategories droneMaxWeight = WeightCategories.light, parcelWeight = WeightCategories.light;
            Priorities priority = Priorities.regular;
            Console.WriteLine("Welcome to our program!\n");

            //Loop for the choices of the user until he choose to exit:
            while (firstChoice != 5)
            {
                //Reset the variables:
                stationId = 0;
                droneId = 0;
                customerId = 0;
                parcelId = 0;
                droneId = 0;
                senderId = 0;
                targetId = 0;
                stationName = 0;
                chargeSlots = 0;
                stationLongitude = 0;
                stationLattitude = 0;
                customerLongitude = 0;
                customerLattitude = 0;
                droneMaxWeight = WeightCategories.light;
                droneMaxWeightString = "";
                droneModel = "";
                customerName = "";
                customerPhone = "";
                parcelWeight = WeightCategories.light;
                parcelWeightString = "";
                priority = Priorities.regular;
                priorityString = "";

                //The main menu, and the first choice:
                firstChoice = options("~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~ Choose one of the follow options: ~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~<>~\n" +
                    "1. Insert options.\n" +
                    "2. Update options.\n" +
                    "3. Display options.\n" +
                    "4. List view options.\n" +
                    "5. Exit.");
                switch (firstChoice)
                {
                    //Insert options:
                    case 1:
                        //first menu for the second choice:
                        secondChoice = options("==========Insert options==========\n" +
                            "1. Add a base station to the list of stations.\n" +
                            "2. Add a drone to the list of existing drones.\n" +
                            "3. Addition of a new customer to the customer list.\n" +
                            "4. Receipt of package for shipment.");
                        switch (secondChoice)
                        {
                            //option 1: Add a base station:
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
                            //option 2: Add a drone:
                            case 2:
                                Console.WriteLine("Enter drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                Console.WriteLine("Enter drone model: ");
                                droneModel = Console.ReadLine();
                                Console.WriteLine("Enter max weight for the drone: ");
                                droneMaxWeightString = Console.ReadLine();
                                switch (droneMaxWeightString)
                                {
                                    case "light": droneMaxWeight = WeightCategories.light; break;
                                    case "liver": droneMaxWeight = WeightCategories.liver; break;
                                    case "medium": droneMaxWeight = WeightCategories.medium; break;
                                    default:
                                        Console.WriteLine("INPUT ERROR!   try again");
                                        break;
                                }
                                DAL.DalObject.DalObject.AddDrone(droneId, droneModel, droneMaxWeight);
                                break;
                            //option 3: Add a customer:
                            case 3:
                                Console.WriteLine("Enter customer Id: ");
                                int.TryParse(Console.ReadLine(), out customerId);
                                Console.WriteLine("Enter customer name: ");
                                customerName = Console.ReadLine();
                                Console.WriteLine("Enter customer phone: ");
                                customerPhone = Console.ReadLine();
                                Console.WriteLine("Enter customer longitude: ");
                                double.TryParse(Console.ReadLine(), out customerLongitude);
                                Console.WriteLine("Enter customer lattitude: ");
                                double.TryParse(Console.ReadLine(), out customerLattitude);
                                DAL.DalObject.DalObject.Addcustomer(customerId, customerName, customerPhone, customerLongitude, customerLattitude);
                                break;
                            //option 4: Add a parcel:
                            case 4:
                                Console.WriteLine("Enter parcel Id: ");
                                int.TryParse(Console.ReadLine(), out parcelId);
                                Console.WriteLine("Enter sender Id: ");
                                int.TryParse(Console.ReadLine(), out senderId);
                                Console.WriteLine("Enter target Id: ");
                                int.TryParse(Console.ReadLine(), out targetId);
                                Console.WriteLine("Enter parcel weight: ");
                                parcelWeightString = Console.ReadLine();
                                Console.WriteLine("Enter priority: ");
                                priorityString = Console.ReadLine();
                                switch (parcelWeightString)
                                {
                                    case "light": parcelWeight = WeightCategories.light; break;
                                    case "liver": parcelWeight = WeightCategories.liver; break;
                                    case "medium": parcelWeight = WeightCategories.medium; break;
                                    default:
                                        Console.WriteLine("INPUT ERROR!   try again"); 
                                        break;
                                }
                                switch (priorityString)
                                {
                                    case "regular": priority = Priorities.regular; break;
                                    case "quick": priority = Priorities.quick; break;
                                    case "emergency": priority = Priorities.emergency; break;
                                    default:
                                        Console.WriteLine("INPUT ERROR!   try again"); 
                                        break;
                                }
                                DAL.DalObject.DalObject.AddParcel(parcelId, senderId, targetId, parcelWeight, priority);
                                break;
                            default:
                                Console.WriteLine("INPUT ERROR!   try again");
                                break;
                        }
                        break;
                    //Update options:
                    case 2:
                        //second menu for the second choice:
                        secondChoice = options("==========Update options==========\n" +
                            "1. Assign a package to a drone.\n" +
                            "2. Collection of a package by a drone.\n" +
                            "3. Delivery package to customer.\n" +
                            "4. Sending a drone for charging at a base station.\n" +
                            "5. Release drone from charging at base station.");
                        switch (secondChoice)
                        {
                            //option 1: Assign a package to a drone:
                            case 1:
                                Console.WriteLine("Enter parcel Id: ");
                                int.TryParse(Console.ReadLine(), out parcelId);
                                DAL.DalObject.DalObject.schedule(parcelId);
                                break;
                            //option 2: Collection of a package by a drone:
                            case 2:
                                Console.WriteLine("Enter parcel Id: ");
                                int.TryParse(Console.ReadLine(), out parcelId);
                                DAL.DalObject.DalObject.pickUp(parcelId);
                                break;
                            //option 3: Delivery package to customer:
                            case 3:
                                Console.WriteLine("Enter parcel Id: ");
                                int.TryParse(Console.ReadLine(), out parcelId);
                                DAL.DalObject.DalObject.deliver(parcelId);
                                break;
                            //option 4: Sending a drone for charging at a base station:
                            case 4:
                                Console.WriteLine("Enter drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                Console.WriteLine("Choose one of the follow stations:");
                                foreach (int item in DAL.DalObject.DalObject.IdListForStations()){ Console.WriteLine("Station " + item); }
                                int.TryParse(Console.ReadLine(), out stationId);
                                DAL.DalObject.DalObject.charge(droneId, stationId);
                                break;
                            //option 5: Release drone from charging at base station: 
                            case 5:
                                Console.WriteLine("Enter drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                DAL.DalObject.DalObject.unCharge(droneId);
                                break;
                            default:
                                Console.WriteLine("INPUT ERROR!   try again");
                                break;
                        }
                        break;
                    //Display options:
                    case 3:
                        //third menu for the second choice:
                        secondChoice = options("==========Display options==========\n" +
                            "1. Base station view.\n" +
                            "2. Drone view.\n" +
                            "3. Customer view.\n" +
                            "4. Package view.");
                        switch (secondChoice)
                        {
                            //option 1: view base station:
                            case 1:
                                Console.WriteLine("Enter the Id of the station:");
                                int.TryParse(Console.ReadLine(), out stationId);
                                Station station = DAL.DalObject.DalObject.displayStation(stationId);
                                Console.WriteLine(station.ToString());
                                break;
                            //option 2: view a drone:
                            case 2:
                                Console.WriteLine("Enter the Id of the drone:");
                                int.TryParse(Console.ReadLine(), out droneId);
                                Drone drone = DAL.DalObject.DalObject.displayDrone(droneId);
                                Console.WriteLine(drone.ToString());
                                break;
                            //option 3: view a customer:
                            case 3:
                                Console.WriteLine("Enter the Id of the customer:");
                                int.TryParse(Console.ReadLine(), out customerId);
                                customer customer = DAL.DalObject.DalObject.displaycustomer(customerId);
                                Console.WriteLine(customer.ToString());
                                break;
                            //option 4: view a package:
                            case 4:
                                Console.WriteLine("Enter the Id of the parcel:");
                                int.TryParse(Console.ReadLine(), out parcelId);
                                Parcel parcel = DAL.DalObject.DalObject.displayParcel(parcelId);
                                Console.WriteLine(parcel.ToString());
                                break;
                            default:
                                Console.WriteLine("INPUT ERROR!   try again");
                                break;
                        }
                        break;
                    //List view options:
                    case 4:
                        //Fourth menu for the second choice:
                        secondChoice = options("==========List view options==========\n" +
                        "1. Displays a list of base stations.\n" +
                        "2. Displays the list of drones.\n" +
                        "3. View customer list.\n" +
                        "4. Displays the list of packages.\n" +
                        "5. Displays a list of packages not yet associated with a drone.\n" +
                        "6. Display base stations with available charging stations.");    
                        switch (secondChoice)
                        {
                            //option 1: Displays a list of base stations:
                            case 1:
                                foreach (Station item in DAL.DalObject.DalObject.displayStationList()){ Console.WriteLine(item.ToString()); }
                                break;
                            //option 2: Displays the list of drones:
                            case 2:
                                foreach (Drone item in DAL.DalObject.DalObject.displayDroneList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 3: View customer list:
                            case 3:
                                foreach (customer item in DAL.DalObject.DalObject.displaycustomerList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 4: Displays the list of packages:
                            case 4:
                                foreach (Parcel item in DAL.DalObject.DalObject.displayParcelList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 5: Displays a list of packages not yet associated with a drone:
                            case 5:
                                foreach(Parcel item in DAL.DalObject.DalObject.displayParcelList()){ if (item.DroneId == -1) { Console.WriteLine(item.ToString()); } }
                                break;
                            //option 6: Display base stations with available charging stations:
                            case 6:
                                foreach (Station item in DAL.DalObject.DalObject.displayStationList()) { if (item.ChargeSlots > 0) { Console.WriteLine(item.ToString()); } }
                                break;
                            default:
                                Console.WriteLine("INPUT ERROR!   try again");
                                break;
                        }
                        break;
                    default:
                        Console.WriteLine("INPUT ERROR!   try again");
                        break;
                }
            }
        }
    }
}
