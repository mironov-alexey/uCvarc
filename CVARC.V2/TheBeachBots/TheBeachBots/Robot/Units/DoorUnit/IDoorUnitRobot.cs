using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheBeachBots;

namespace CVARC.V2
{
    public interface IDoorUnitRobot : IActor
    {
        DoorUnit DoorUnit { get; }
    }
}
