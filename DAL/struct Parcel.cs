using System;
using System.Runtime.CompilerServices;

namespace DO
{
    public struct Parcel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int TargetId { get; set; }
        public WeightCategories Weight { get; set; }
        public Priorities Priority { get; set; }
        public DateTime? Defined { get; set; }
        public int DroneId { get; set; }
        public DateTime? Assigned { get; set; }
        public DateTime? Collected { get; set; }
        public DateTime? Provided { get; set; }
        
        public override string ToString()
        {
            return "Parcel Id: " + this.Id +
                "\nSender Id: " + this.SenderId +
                "\nTarget Id: " + this.TargetId +
                "\nParcel weight: " + this.Weight +
                "\nParcel priority: " + this.Priority +
                "\nDrone Id: " + this.DroneId +
                "\nRequested time: " + this.Defined +
                "\nScheduled time: " + this.Assigned +
                "\nPick up time: " + this.Collected +
                "\nDeliver time: " + this.Provided;
        }
    }
}