using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int FirstChoice = 0, SecondChoice = 0;
            while (FirstChoice != 5)
            {
                Console.WriteLine("Welcome to our program!\n=====Choose one of the follow options:=====");
                Console.WriteLine("1. Insert options.");
                Console.WriteLine("2. Update options.");
                Console.WriteLine("3. Display options.");
                Console.WriteLine("4. List view options.");
                Console.WriteLine("5. Exit.");
                int.TryParse(Console.ReadLine(), out FirstChoice);
                switch (FirstChoice)
                {
                    case 1:
                        Console.WriteLine("=====Insert options=====");
                        Console.WriteLine("1. Add a base station to the list of stations.");
                        Console.WriteLine("2. Add a drone to the list of existing drones.");
                        Console.WriteLine("3. Addition of a new customer to the customer list.");
                        Console.WriteLine("4. Receipt of package for shipment.");
                        int.TryParse(Console.ReadLine(), out SecondChoice);
                        break;
                    case 2:
                        Console.WriteLine("=====Update options=====");
                        Console.WriteLine("1. Assign a package to a drone.");
                        Console.WriteLine("2. Collection of a package by drone.");
                        Console.WriteLine("3. Delivery package to customer.");
                        Console.WriteLine("4. Sending a drone for charging at a base station.");
                        Console.WriteLine("5. Release drone from charging at base station.");
                        int.TryParse(Console.ReadLine(), out SecondChoice);
                        break;
                    case 3:
                        Console.WriteLine("=====Display options=====");
                        Console.WriteLine("1. Base station view.");
                        Console.WriteLine("2. Drone view.");
                        Console.WriteLine("3. Customer view.");
                        Console.WriteLine("4. Package view.");
                        int.TryParse(Console.ReadLine(), out SecondChoice);
                        break;
                    case 4:
                        Console.WriteLine("=====List view options=====");
                        Console.WriteLine("1. Displays a list of base stations.");
                        Console.WriteLine("2. Displays the list of drones.");
                        Console.WriteLine("3. View customer list.");
                        Console.WriteLine("4. Displays the list of packages.");
                        Console.WriteLine("5. Displays a list of packages not yet associated with a drone.");
                        Console.WriteLine("6. Display base stations with available charging stations.");    
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
