using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots
{
    class TBBScoresTrigger : Trigger
    {
        TBBWorld world;
        public double Interval { get; private set; }

        public TBBScoresTrigger(TBBWorld world, double interval=0.5)
        {
            this.world = world;
            Interval = ScheduledTime = interval;
        }

        public override TriggerKeep Act(double time)
        {
            world.Scores.DeleteTemporaryRecords();

            ScheduledTime += Interval;
            return TriggerKeep.Keep;
        }
    }
}
