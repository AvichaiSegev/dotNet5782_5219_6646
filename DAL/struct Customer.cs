using System;

namespace DO
{ 
    public struct Customer
    {
        public Customer(Customer customer)
        {
            Id = customer.Id;
            Name = customer.Name;
            Phone = customer.Phone;
            Longitude = customer.Longitude;
            Lattitude = customer.Lattitude;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
        public override string ToString()
        {
            return "customer Id: " + this.Id +
                        "\ncustomer name: " + this.Name +
                        "\ncustomer phone: " + this.Phone +
                        "\ncustomer longitude: " + this.Longitude +
                        "\ncustomer lattitude: " + this.Lattitude;
        }
    }
}