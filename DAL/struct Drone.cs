using System;
using System.Runtime.CompilerServices;

namespace DO
{
    public struct Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public WeightCategories MaxWeight { get; set; }
        
        public override string ToString()
        {
            return "Drone Id: " + this.Id +
                        "\nDrone model: " + this.Model +
                        "\nMax weight of the drone: " + this.MaxWeight;
        }
    }
}