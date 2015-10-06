using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;

namespace TheBeachBots
{
    public abstract class ScoreSensor : Sensor<int, IActor>
    {
        public readonly bool MeasureSelf;

        public ScoreSensor(bool measureSelf)
        {
            MeasureSelf = measureSelf;
        }

        public override int Measure()
        {
            var id = Actor.World.Actors
                .Where(z => (z.ControllerId == Actor.ControllerId) == MeasureSelf)
                .Select(z => z.ControllerId)
                .FirstOrDefault();
            return Actor.World.Scores.GetTotalScore(id);
        }
    }

    public class OpponentScoreSensor : ScoreSensor
    {
        public OpponentScoreSensor() : base(false) { }
    }

    public class SelfScoreSensor : ScoreSensor
    {
        public SelfScoreSensor() : base(true) { }
    }
}
