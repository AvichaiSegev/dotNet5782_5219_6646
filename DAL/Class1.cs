using System;

namespace IDAL
{
    namespace DO
    {
        public enum WeightCategories { light, medium, liver }
        public enum Priorities { regular, quick, emergency }
        public enum DroneStatuses { available, maintenance, shipment }
        public struct Costumer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
        }

        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }
            public int DroneID { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }

        }

        public struct DroneCharge
        {
            public int Droneld { get; set; }
            public int Stationld { get; set; }
        }

        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategories MaxWeight { get; set; }
            public DroneStatuses Status { get; set; }
            public double Battery { get; set; }
        }
        public struct Station
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public double Longitude { get; set; }
            public double Lattitude { get; set; }
            public int ChargeSlots { get; set; }


        }
    }
}