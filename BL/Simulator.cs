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
        int SPEED = 20;
        public Simulator(BL bl, int DroneID, Action WPFUpdate, Func<bool> StopCheck)
        {
            Drone drone = new Drone();
            drone.battery = 0;
            WPFUpdate();
            while(!StopCheck())
            {
                drone = bl.displayDrone(DroneID);
                switch (drone.status)
                {
                    case DroneStatus.free:
                        try
                        {
                            bl.assignParcelToDrone(drone.id);
                            WPFUpdate();
                            Thread.Sleep(SLEEP);
                        }
                        catch
                        {
                            try
                            {
                                if (drone.battery < 100)
                                {
                                    try { bl.sendDroneToCharging(drone.id); }
                                    catch { bl.UpdateDrone(drone); }
                                    WPFUpdate();
                                    Thread.Sleep(SLEEP);
                                }
                                else
                                {
                                    while (drone.status == DroneStatus.free)
                                    {
                                        try { bl.assignParcelToDrone(drone.id); }
                                        catch { Thread.Sleep(5000); }
                                    }
                                }
                            }
                            catch { Thread.Sleep(SLEEP); }
                        }
                        break;
                    case DroneStatus.delivery:
                        BO.Parcel parcel = bl.displayParcel(drone.parcel.id);
                        if(parcel.collectedParcelTime == null)
                        {
                            collect(bl, drone, parcel);
                        }
                        else
                        {
                            if(parcel.delivered == null)
                            {
                                update(drone, parcel);
                            }
                        }
                        break;
                    case DroneStatus.matance:
                        if (drone.battery == 100)
                        {
                            bl.releaseDroneFromCharging(drone.id, 0);
                            WPFUpdate();
                            Thread.Sleep(SLEEP);
                        }
                        else
                        {
                            double N = drone.battery + 1 * bl.chargingRate;
                            drone.battery = N <= 100 ? N : 100;
                            bl.UpdateDrone(drone);
                            WPFUpdate();
                            Thread.Sleep(SLEEP);
                        }
                        break;
                    default:
                        break;
                }
            }
            void collect(BL bl, Drone drone, Parcel parcel)
            {
                Location senderLocation = new Location(bl.displayCustomer(parcel.getted.id).location.longitude, bl.displayCustomer(parcel.getted.id).location.latitude);
                double distance = bl.DistanceTo(drone.location.longitude, drone.location.latitude, bl.displayCustomer(parcel.delivered.id).location.longitude, bl.displayCustomer(parcel.delivered.id).location.latitude);
                while (SLEEP < distance)
                {
                    drone.battery-=SPEED;
                    bl.UpdateDrone(drone);
                    distance -= SPEED;
                    WPFUpdate();
                    Thread.Sleep(SLEEP);
                }
                drone.battery -= distance;
                bl.UpdateDrone(drone);
                parcel.collectedParcelTime = DateTime.Now;
                try { bl.UpdateParcel(parcel); }
                catch (Exception e) { throw new Exception($"The Parcel id not exist:{parcel.Id}", e); }
                WPFUpdate();
                Thread.Sleep(SLEEP);
            }
            void update(Drone drone, BO.Parcel parcel)
            {
                Location targetLocation = new Location(bl.displayCustomer(parcel.getted.id).location.longitude, bl.displayCustomer(parcel.getted.id).location.latitude);
                double distance = bl.DistanceTo(drone.location.longitude, drone.location.latitude, bl.displayCustomer(parcel.getted.id).location.longitude, bl.displayCustomer(parcel.getted.id).location.latitude);
                while (SPEED < distance)
                {
                    drone.battery -= bl.electricityUseForVacantDrone;
                    bl.UpdateDrone(drone);
                    distance -= SPEED;
                    WPFUpdate();
                    Thread.Sleep(SLEEP);
                }
                drone.battery -= bl.electricityUseForVacantDrone;
                bl.UpdateDrone(drone);
                parcel.definedParcelTime = DateTime.Now;
                try { bl.UpdateParcel(parcel); }
                catch (Exception e) { throw new Exception($"The Parcel id not exist:{parcel.Id}", e); }
                WPFUpdate();
                Thread.Sleep(SLEEP);
            }
        }
    }
}
