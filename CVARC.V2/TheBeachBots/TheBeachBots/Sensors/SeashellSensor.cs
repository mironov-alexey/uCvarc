using CVARC.V2;

namespace TheBeachBots
{
    class SeashellSensor : Sensor<bool, TBBRobot>
    {
        public override bool Measure()
        {
            return Actor.SeashellGripper.GrippedObjectId != null;
        }
    }
}