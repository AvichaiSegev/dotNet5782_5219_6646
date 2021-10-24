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
            public int DroneID { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }

        }
    }
}