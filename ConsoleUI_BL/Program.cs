using System;
using BL;
using IBL.BO;


namespace ConsoleUI_BL
{
    class Program
    {
        static int options(string text)
        {
            int Choice;
            Console.WriteLine(text);
            int.TryParse(Console.ReadLine(), out Choice);
            return Choice;
        }
        static private IBL.IBL logi = new BL.BL;
        public static void Main(string[] args)
        {
            int firstChoice = 0, secondChoice;
            int stationId, droneId, customerId, parcelId, senderId = 0, targetId = 0;
            int stationName = 0, chargeSlots = 0;
            string droneMaxWeightString = "", customerName = "", customerPhone = "", parcelWeightString = "", priorityString = "";
            double stationLongitude = 0, stationLattitude = 0, customerLongitude = 0, customerLattitude = 0, chargingTime;
            string droneModel = "";
            WeightCategories droneMaxWeight = WeightCategories.light, parcelWeight = WeightCategories.light;
            Priorities priority = Priorities.regular;
            Console.WriteLine("Welcome to our program!\n");
            while (firstChoice != 5)
            {
                
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
                                try
                                {
                                logi.AddStation(stationId, stationName, stationLongitude, stationLattitude, chargeSlots);
                                }
                                catch(Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
                                ////////////////////////////////////////
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
                                Console.WriteLine("Enter station for first charging: ");
                                int.TryParse(Console.ReadLine(), out stationId);
                                try
                                {
                                    logi.AddDrone(droneId, droneModel, droneMaxWeight);
                                }
                                catch(Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
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
                                try
                                {
                                    logi.Addcustomer(customerId, customerName, customerPhone, customerLongitude, customerLattitude);
                                }
                                catch (Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
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
                                try
                                {
                                    logi.AddParcel(parcelId, senderId, targetId, parcelWeight, priority);
                                }
                                catch(Exception error)
                                    {
                                    Console.WriteLine(error.Message);
                                }
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
                            "3. Delivery package to customer.\n" +//////////////
                            "4. Sending a drone for charging at a base station.\n" +
                            "5. Release drone from charging at base station.");
                        switch (secondChoice)
                        {
                            case 1:
                                Console.WriteLine("Enter drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                Console.WriteLine("Enter parcel model: ");
                                droneModel = Console.ReadLine();
                                try
                                {
                                    logi.UpdateDroneModel(droneId, droneModel);
                                }
                                catch(Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
                                break;
                            case 2:
                                Console.WriteLine("Enter station Id: ");
                                int.TryParse(Console.ReadLine(), out stationId);
                                Console.WriteLine("Enter station Id: ");
                                int.TryParse(Console.ReadLine(), out stationId);
                                try
                                {
                                    logi.UpdateStationName(stationId, stationName);
                                }
                                catch(Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
                                break;
                            case 3:
                                Console.WriteLine("Enter Customer Id: ");
                                int.TryParse(Console.ReadLine(), out customerId);
                                Console.WriteLine("Enter Customer Name: ");
                                customerName = Console.ReadLine();
                                Console.WriteLine("Enter Customer Phone: ");
                                customerPhone = Console.ReadLine();
                                logi.UpdatecustomerNameAndPhone(customerId, customerName, customerPhone);
                                break;
                            case 4:
                                Console.WriteLine("Enter Drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                logi.sendDroneToCharging(droneId);
                                break;
                            case 5:
                                Console.WriteLine("Enter Drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                Console.WriteLine("Enter charging Time: ");
                                double.TryParse(Console.ReadLine(), out chargingTime);//chargingTime - Hours
                                logi.releaseDroneFromCharging(droneId, chargingTime);
                                break;
                            case 6:
                                Console.WriteLine("Enter Drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                logi.assignParcelToDrone(droneId);
                                break;
                            case 7:
                                Console.WriteLine("Enter Drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                logi.collectParcelByDrone(droneId);
                                break;
                            case 8:
                                Console.WriteLine("Enter Drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                logi.provideParcelByDrone(droneId);
                                break;
                         //   //option 1: Assign a package to a drone:
                         //   case 10:
                         //       Console.WriteLine("Enter parcel Id: ");
                         //       int.TryParse(Console.ReadLine(), out parcelId);
                         //       logi.schedule(parcelId);
                         //       break;
                         //   //option 2: Collection of a package by a drone:
                         //   case 20:
                         //       Console.WriteLine("Enter parcel Id: ");
                         //       int.TryParse(Console.ReadLine(), out parcelId);
                         //       logi.pickUp(parcelId);
                         //       break;
                         //   //option 3: Delivery package to customer:
                         //   case 30:
                         //       Console.WriteLine("Enter parcel Id: ");
                         //       int.TryParse(Console.ReadLine(), out parcelId);
                         //       DAL.DalObject.DalObject.deliver(parcelId);
                         //       break;
                         //   //option 4: Sending a drone for charging at a base station:
                         //   case 40:
                         //       Console.WriteLine("Enter drone Id: ");
                         //       int.TryParse(Console.ReadLine(), out droneId);
                         //       Console.WriteLine("Choose one of the follow stations:");
                         //       foreach (int item in logi.IdListForStations()) { Console.WriteLine("Station " + item); }
                         //       int.TryParse(Console.ReadLine(), out stationId);
                         //       DAL.DalObject.DalObject.charge(droneId, stationId);
                         //       break;
                         //   //option 5: Release drone from charging at base station: 
                         //   case 50:
                         //       Console.WriteLine("Enter drone Id: ");
                         //       int.TryParse(Console.ReadLine(), out droneId);
                         //       DAL.DalObject.DalObject.unCharge(droneId);
                         //       break;
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
                                Station station = logi.displayStation(stationId);
                                Console.WriteLine(station.ToString());
                                break;
                            //option 2: view a drone:
                            case 2:
                                Console.WriteLine("Enter the Id of the drone:");
                                int.TryParse(Console.ReadLine(), out droneId);
                                Drone drone = logi.displayDrone(droneId);
                                Console.WriteLine(drone.ToString());
                                break;
                            //option 3: view a customer:
                            case 3:
                                Console.WriteLine("Enter the Id of the customer:");
                                int.TryParse(Console.ReadLine(), out customerId);
                                Customer customer = logi.displaycustomer(customerId);
                                Console.WriteLine(customer.ToString());
                                break;
                            //option 4: view a package:
                            case 4:
                                Console.WriteLine("Enter the Id of the parcel:");
                                int.TryParse(Console.ReadLine(), out parcelId);
                                Parcel parcel = logi.displayParcel(parcelId);
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
                                foreach (Station item in logi.displayStationList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 2: Displays the list of drones:
                            case 2:
                                foreach (Drone item in logi.displayDroneList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 3: View customer list:
                            case 3:
                                foreach (Customer item in logi.displaycustomerList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 4: Displays the list of packages:
                            case 4:
                                foreach (Parcel item in logi.displayParcelList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 5: Displays a list of packages not yet associated with a drone:
                            case 5:
                                foreach (Parcel item in logi.displayParcelList()) { if (item.DroneId == -1) { Console.WriteLine(item.ToString()); } }
                                break;
                            //option 6: Display base stations with available charging stations:
                            case 6:
                                foreach (Station item in logi.displayStationList()) { if (item.ChargeSlots > 0) { Console.WriteLine(item.ToString()); } }
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
