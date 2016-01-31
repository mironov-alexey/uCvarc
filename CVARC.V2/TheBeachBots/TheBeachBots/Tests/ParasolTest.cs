using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;

namespace TheBeachBots.General
{
    public class ParasolTest : DefaultTBBTest
    {
        [CvarcTestMethod]
        public void Parasol_GotScoresIfOpenParasol()
        {
            Robot.ActivateParasol();
            AssertEqual(s => s.SelfScores, 20);
        }

        [CvarcTestMethod]
        public void Parasol_NoScoresIfCloseParasol()
        {
            Robot.ActivateParasol();
            Robot.DeactivateParasol();
            AssertEqual(s => s.SelfScores, 0);
        }
    }
}
