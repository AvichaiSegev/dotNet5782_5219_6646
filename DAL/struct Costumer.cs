using System;

namespace IDAL
{
    namespace DO
    { 
        public struct Costumer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public override string ToString()
            {
                return "Costumer Id: " + this.Id +
                            "\nCostumer name: " + this.Name +
                            "\nCostumer phone: " + this.Phone +
                            "\nCostumer longitude: " + this.Longitude +
                            "\nCostumer lattitude: " + this.Lattitude;
            }
        }
    }
}