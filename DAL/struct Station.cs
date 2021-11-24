using System;

namespace IDAL
{
    namespace DO
    {
        public struct Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int freeChargeSlots { get; set; }
            public override string ToString()
            {
                return "Station Id: " + this.Id +
                        "\nStation name: " + this.Name +
                        "\nStation longitude: " + this.Longitude +
                        "\nStation lattitude: " + this.Lattitude +
                        "\nStation charge slots: " + this.freeChargeSlots;
            }
        }
    }
}