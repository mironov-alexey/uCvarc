using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots
{
    public class TBBWorldState : IWorldState
    {
        public readonly int Seed;

        public TBBWorldState(int seed)
        {
            Seed = seed;
        }
    }
}
