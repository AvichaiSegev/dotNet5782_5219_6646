using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
            public override string ToString()
            {
                return "Drone Id: " + this.Id +
                            "\nDrone model: " + this.Model +
                            "\nMax weight of the drone: " + this.MaxWeight +
                            "\nStatus of the drone: " + this.Status +
                            "\nBattery status: " + this.Battery;
            }
        }
    }
}