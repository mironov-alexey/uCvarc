using CVARC.V2;

namespace TheBeachBots
{
    class FishingRodSensor : Sensor<bool, TBBRobot>
    {
        public override bool Measure()
        {
            return Actor.FishingRod.GrippedObjectId != null;
        }
    }
}
