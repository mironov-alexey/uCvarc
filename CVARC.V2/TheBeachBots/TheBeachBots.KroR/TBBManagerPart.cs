using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots.KroR
{
    public class TBBManagerPart : ManagerPart
    {
        public TBBManagerPart() : base(()=>new TBBWorldManager())
        {

        }

        public override IActorManager CreateActorManagerFor(IActor actor)
        {
            return new TBBActorManager();
        }
    }
}
