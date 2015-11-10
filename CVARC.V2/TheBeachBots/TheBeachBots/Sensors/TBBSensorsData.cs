using System.Runtime.Serialization;
using CVARC.V2;

namespace TheBeachBots
{
    public class TBBSensorsData
    {
        [DataMember]
        [FromSensor(typeof(MapSensor<TBBObject>))]
        public Map<TBBObject> Map { get; set; }

        [DataMember]
        [FromSensor(typeof(SelfIdSensor))]
        public string RobotId { get; set; }

        [DataMember]
        [FromSensor(typeof(SelfLocationSensor))]
        public LocatorItem SelfLocation { get; set; }

        [DataMember]
        [FromSensor(typeof(OpponentLocationSensor))]
        public LocatorItem OpponentLocation { get; set; }

        [DataMember]
        [FromSensor(typeof(SelfScoreSensor))]
        public int SelfScores { get; set; }

        [DataMember]
        [FromSensor(typeof(OpponentScoreSensor))]
        public int OpponentScores { get; set; }

        [DataMember]
        [FromSensor(typeof(FishingRodSensor))]
        public bool FishAttached { get; set; }

        [DataMember]
        [FromSensor(typeof(SeashellSensor))]
        public bool SeashellAttached { get; set; }

        [DataMember]
        [FromSensor(typeof(SandGripperSensor))]
        public int CollectedSandCount { get; set; }

        [DataMember]
        [FromSensor(typeof(ParasolSensor))]
        public bool ParasolActivated { get; set; }
    }
}
