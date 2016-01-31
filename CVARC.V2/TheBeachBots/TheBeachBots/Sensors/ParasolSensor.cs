using CVARC.V2;

namespace TheBeachBots
{
    class ParasolSensor : Sensor<bool, TBBRobot>
    {
        public override bool Measure()
        {
            return Actor.ParasolUnit.IsActivated;
        }
    }
}
