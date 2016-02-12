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

