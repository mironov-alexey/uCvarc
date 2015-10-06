using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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
        public RTSLocatorItem SelfLocation { get; set; }

        [DataMember]
        [FromSensor(typeof(OpponentLocationSensor))]
        public RTSLocatorItem OpponentLocation { get; set; }

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
    }
}
