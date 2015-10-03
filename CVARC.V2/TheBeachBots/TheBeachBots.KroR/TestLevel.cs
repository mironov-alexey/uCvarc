using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots.KroR
{
    public class TestLevel : Competitions
    {
        public TestLevel()
			: base(new TBBLogicPartHelper(), new KroREnginePart(), new TBBManagerPart())
        { }
    }
}
