using System;
using BO;
using System.Threading;
using static BL.BL;
using System.Linq;

namespace BL
{
    class Simulator
    {
        int speed;
        int SLEEP = 500;
        public Simulator(BL bl, int DroneID, Action WPFUpdate, Func<bool> StopCheck)
        {
            Drone drone;
            while(!StopCheck())
            {
                drone = bl.displayDrone(DroneID);
                switch (drone.status)
                {
                    case DroneStatus.free:
                        break;
                    case DroneStatus.delivery:
                        BO.Parcel parcel = bl.displayParcel(drone.parcel.id);
                        if(parcel.collectedParcelTime == null)
                        {
                            collect(drone, parcel);
                        }
                        break;
                    case DroneStatus.matance:
                        break;
                    default:
                        break;
                }
            }
            void collect(Drone drone, Parcel parcel)
            {

            }
        }
    }
}
