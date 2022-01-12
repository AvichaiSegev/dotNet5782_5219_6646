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
        int DELAY = 500;
        Simulator(BL bl, int DroneID, Action WPFUpdate, Func<bool> StopCheck)
        {
            while(true)
            {
                Thread.Sleep(DELAY);
            }
        }
    }
}
