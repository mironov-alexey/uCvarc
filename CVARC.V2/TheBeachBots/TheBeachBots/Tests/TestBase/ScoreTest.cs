using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots
{
    class ScoreTest : TBBTestBase
    {
        public ScoreTest(int scoreCount, params TBBCommand[] commands)
            : base((data, asserter) => asserter.IsEqual(scoreCount, data.SelfScores, 0), commands) { }
    }
}
