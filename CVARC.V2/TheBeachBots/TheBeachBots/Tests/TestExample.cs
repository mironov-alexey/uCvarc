using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    partial class TBBLogicPartHelper
    {
        public void LoadTestExample(LogicPart logic, TBBRules rules)
        {
            var builder = new TBBTestBuilder(rules, new TBBWorldState(42)) { Reflected = true };

            logic.Tests["TestExample0"] =  builder
                .Commands
                    .Move(30)
                    .Rotate(-Angle.HalfPi)
                    .Move(15)
                    .Rotate(Angle.HalfPi)
                    .Move(80)
                    .Stand(1)
                .Back
                    .AssertScores(0)
                    .CreateTest();

            builder.Settings.SpeedUp = true;

            logic.Tests["TestExample1"] = builder
                .Commands
                    .Move(30)
                    .Rotate(-Angle.HalfPi)
                    .Move(15)
                    .Rotate(Angle.HalfPi)
                    .Move(80)
                    .Stand(1)
                .Back
                    .AssertScores(0)
                    .CreateTest();

            builder.Settings.SpeedUp = false;
            builder.Reflected = false;
            builder.WorldState = new TBBWorldState(0);

            logic.Tests["TestExample2"] = builder
                .Commands
                    .Move(30)
                    .Rotate(-Angle.HalfPi)
                    .Move(15)
                    .Rotate(Angle.HalfPi)
                    .Move(80)
                    .Stand(1)
                .Back
                    .AssertScores(0)
                    .CreateTest();
        }
    }
}
