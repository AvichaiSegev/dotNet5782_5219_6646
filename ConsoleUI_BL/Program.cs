using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using BL;
using BO;


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
        static private BlApi.IBL logi = BlApi.BlFactory.GetBl();
        public static void Main(string[] args)
        {
            //XmlSerializer cusSer = new XmlSerializer(typeof(List<DO.Customer>));
            //List<DO.Customer> cusData = new List<DO.Customer>();
            //TextWriter cusWriter = new StreamWriter(@"Data\Customers.xml");
            //cusData.Add(new DO.Customer { Id = 1, Lattitude = 1, Longitude = 1, Name = "1", Phone = "1" });
            //cusSer.Serialize(cusWriter, cusData);
            //cusWriter.Close();
            XmlSerializer droCharSer = new XmlSerializer(typeof(List<DO.DroneCharge>));
            List<DO.DroneCharge> droCharData = new List<DO.DroneCharge>();
            //droCharData.Add(new DO.DroneCharge { droneId = 1, StationId = 1 });
            TextWriter droCharWriter = new StreamWriter(@"Data\DroneCharges.xml");
            droCharSer.Serialize(droCharWriter, droCharData);
            droCharWriter.Close();
            //XmlSerializer parSer = new XmlSerializer(typeof(List<DO.Parcel>));
            //List<DO.Parcel> parData = new List<DO.Parcel>();
            //parData.Add(new DO.Parcel { Assigned = DateTime.MinValue, Collected = DateTime.MinValue, Defined = DateTime.MinValue, DroneId = 1, Id = 1, Priority = DO.Priorities.regular, Provided = DateTime.MinValue, SenderId = 1, TargetId = 2, Weight = DO.WeightCategories.light });
            //TextWriter parWriter = new StreamWriter(@"Data\Parcels.xml");
            //parSer.Serialize(parWriter, parData);
            //parWriter.Close();
            //XmlSerializer staSer = new XmlSerializer(typeof(List<DO.Station>));
            //List<DO.Station> staData = new List<DO.Station>();
            //staData.Add(new DO.Station { freeChargeSlots = 1, Id = 1, Lattitude = 1, Longitude = 1, Name = 1 });
            //TextWriter staWriter = new StreamWriter(@"Data\Stations.xml");
            //staSer.Serialize(staWriter, staData);
            //staWriter.Close();
            //XmlSerializer droSer = new XmlSerializer(typeof(List<DO.Drone>));
            //List<DO.Drone> droData = new List<DO.Drone>();
            //droData.Add(new DO.Drone { Id = 1, MaxWeight = DO.WeightCategories.light, Model = "segev" });
            //TextWriter droWriter = new StreamWriter(@"Data\Drones.xml");
            //droSer.Serialize(droWriter, droData);
            //droWriter.Close();
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

                                    logi.AddStation(new Station() { id = stationId, location = new Location(stationLongitude, stationLattitude), name = stationName, numFreeChargingStands = chargeSlots, dronesInCharging = new List<DroneInCharging>()});
                                    
                                }
                                catch(Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
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
                                    logi.AddDrone(new Drone() { id = droneId, model = droneModel, maxWeight = droneMaxWeight}, stationId);
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
                                    logi.Addcustomer(new Customer() { id = customerId, location = new Location(customerLongitude, customerLattitude), name = customerName, phone = customerPhone});
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
                                    logi.AddParcel(new Parcel() { Id = parcelId, priority = priority, weight = parcelWeight}, senderId, targetId);
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
                            "1. Update drone.\n" +
                            "2. Update station.\n" +
                            "3. Updaet customer.\n" +
                            "4. Sending a drone for charging at a base station.\n" +
                            "5. Release drone from charging at base station.\n" +
                            "6. Assign a package to a drone.\n" +
                            "7. Collection of a package by a drone.\n" +
                            "8. Delivery package to customer.");
                          //  "1. Assign a package to a drone.\n" +
                          //  "2. Collection of a package by a drone.\n" +
                          //  "3. Delivery package to customer.\n" +//////////////
                          //  "4. Sending a drone for charging at a base station.\n" +
                          //  "5. Release drone from charging at base station.");
                        switch (secondChoice)
                        {
                            case 1:
                                Console.WriteLine("Enter drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                Console.WriteLine("Enter drone model: ");
                                droneModel = Console.ReadLine();
                                try
                                {
                                    logi.UpdateDroneModel(new Drone() { id = droneId, model = droneModel });
                                }
                                catch(Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
                                break;
                            case 2:
                                Console.WriteLine("Enter station Id: ");
                                int.TryParse(Console.ReadLine(), out stationId);
                                Console.WriteLine("Enter station name: ");
                                int.TryParse(Console.ReadLine(), out stationName);
                                try
                                {
                                    logi.UpdateStationName(new Station() { id = stationId, name = stationName });
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
                                try
                                {
                                    logi.UpdatecustomerNameAndPhone(new Customer(){id = customerId, name = customerName, phone = customerPhone});
                                }
                                catch(Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
                                break;
                            case 4:
                                Console.WriteLine("Enter Drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                try
                                {
                                    logi.sendDroneToCharging(droneId);
                                }
                                catch (Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
                                break;
                            case 5:
                                Console.WriteLine("Enter Drone Id: ");
                                int.TryParse(Console.ReadLine(), out droneId);
                                Console.WriteLine("Enter charging Time: ");
                                double.TryParse(Console.ReadLine(), out chargingTime);//chargingTime - Hours
                                try
                                {
                                    logi.releaseDroneFromCharging(droneId, chargingTime);
                                }
                                catch (Exception error)
                                {
                                    Console.WriteLine(error.Message);
                                }
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
                                Customer customer = logi.displayCustomer(customerId);
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
                                foreach (StationToList item in logi.displayStationList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 2: Displays the list of drones:
                            case 2:
                                foreach (DroneToList item in logi.displayDroneList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 3: View customer list:
                            case 3:
                                foreach (CustomerToList item in logi.displayCustomerList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 4: Displays the list of packages:
                            case 4:
                                foreach (ParcelToList item in logi.displayParcelList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 5: Displays a list of packages not yet associated with a drone:
                            case 5:
                                foreach (ParcelToList item in logi.displayFreeParcelList()) { Console.WriteLine(item.ToString()); }
                                break;
                            //option 6: Display base stations with available charging stations:
                            case 6:
                                foreach (StationToList item in logi.displayFreeStationList()) { Console.WriteLine(item.ToString()); }
                                break;
                            default:
                                Console.WriteLine("INPUT ERROR!   try again");
                                break;
                        }
                        break;
                    case 5:
                        break;
                    default:
                        Console.WriteLine("INPUT ERROR!   try again");
                        break;
                }
            }
        }
    }
}
