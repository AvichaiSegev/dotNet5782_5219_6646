using System;

namespace IDAL
{
    namespace DO
    {
        struct Costumer
        {
            int Id { get; set; }
            string Name { get; set; }
            string Phone { get; set; }
            double Longitude { get; set; }
            double Lattitude { get; set; }
        }

        struct Parcel
        {
            int id { get; set; }
            int SenderId { get; set; }
            int TargetId { get; set; }
            
            
        }
    }
}