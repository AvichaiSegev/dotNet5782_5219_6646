using System;

namespace IDAL
{
    namespace DO
    {
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public int TargetId { get; set; }
            public WeightCategories Weight { get; set; }
            public Priorities Priority { get; set; }
            public DateTime Requested { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            public override string ToString()
            {
                return "Parcel Id: " + this.Id +
                    "\nSender Id: " + this.SenderId +
                    "\nTarget Id: " + this.TargetId +
                    "\nParcel weight: " + this.Weight +
                    "\nParcel priority: " + this.Priority +
                    "\nDrone Id: " + this.DroneId +
                    "\nRequested time: " + this.Requested +
                    "\nScheduled time: " + this.Scheduled +
                    "\nPick up time: " + this.PickedUp +
                    "\nDeliver time: " + this.Delivered;
            }
        }
    }
}