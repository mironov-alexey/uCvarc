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
            var builder = new TBBTestBuilder(logic, rules, new TBBWorldState(42)) {                
                TimeLimit = 90,
                OperationalTimeLimit = 5,
                SpeedUp = false,
            };

            builder.AddControllerSettings(TwoPlayersId.Left, "This", ControllerType.Client);
            builder.AddControllerSettings(TwoPlayersId.Right, "Standing", ControllerType.Bot);

            //builder.Reflect();

            builder.CreateTest("TestExample")
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(15)
                .Rotate(Angle.HalfPi)
                .Move(80)
                .Stand(1)
                .AssertScores(0)
                .AssertLocation(150 - 30, 30, 10)
                .EndOfTest();
        }
    }
}
