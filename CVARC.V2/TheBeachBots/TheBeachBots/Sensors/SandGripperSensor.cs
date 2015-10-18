using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots
{
    class SandGripperSensor : Sensor<int, TBBRobot>
    {
        public override int Measure()
        {
            return Actor.SandGripper.CollectedDetails.Count;
        }
    }
}

