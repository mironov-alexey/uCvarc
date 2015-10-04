using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheBeachBots;

namespace CVARC.V2
{
    public interface ISeashellRobot : IActor
    {
        SeashellGripper SeashellGripper { get; }
    }
}
