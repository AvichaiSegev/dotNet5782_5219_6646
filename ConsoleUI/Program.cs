using System;

namespace ConsoleUI
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
        static void Main(string[] args)
        {
            int firstChoice = 0, secondChoice = 0;
            while (firstChoice != 5)
            {
                firstChoice = options("Welcome to our program!\n=====Choose one of the follow options:=====\n" +
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
                        break;
                    case 4:
                        secondChoice = options("=====List view options=====\n" +
                        "1. Displays a list of base stations.\n" +
                        "2. Displays the list of drones.\n" +
                        "3. View customer list.\n" +
                        "4. Displays the list of packages.\n" +
                        "5. Displays a list of packages not yet associated with a drone.\n" +
                        "6. Display base stations with available charging stations.");    
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
