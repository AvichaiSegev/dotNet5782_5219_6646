using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome5219();
            Welcome6646();
            Console.ReadKey();
        }
        static partial void Welcome6646();
        private static void Welcome5219()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);

        }
    }
}
