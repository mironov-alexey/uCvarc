using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    partial class TBBLogicPartHelper
    {
        [TestLoaderMethod]
        public void LoadTestExample(LogicPart logic, TBBRules rules)
        {
            AddTest(logic, "TestExample", new ScoreTest(
                0,
                rules.Move(30),
                rules.Rotate(-Angle.HalfPi),
                rules.Move(30),
                rules.Rotate(Angle.HalfPi),
                rules.Move(90),
                rules.Stand(1)
            ) { Reflected = false });
        }
    }
}
