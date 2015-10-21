using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    partial class TBBLogicPartHelper
    {
        public void LoadTestExample(LogicPart logic, TBBRules rules)
        {
            var builder = new TBBTestBuilder(rules, new TBBWorldState(42));            

            builder.Reflect();

            logic.Tests["TestExample0"] =  builder
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(15)
                .Rotate(Angle.HalfPi)
                .Move(80)
                .Stand(1)
                .AssertScores(0)
                .AssertLocation(150 - 30, 30, 10)
                .CreateTest();

            builder.Settings.SpeedUp = true;

            logic.Tests["TestExample1"] = builder
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(15)
                .Rotate(Angle.HalfPi)
                .Move(80)
                .Stand(1)
                .AssertScores(0)
                .AssertLocation(150 - 30, 30, 10)
                .CreateTest();

            builder.Settings.SpeedUp = false;
            builder.Reflect();
            builder.WorldState = new TBBWorldState(0);

            logic.Tests["TestExample2"] = builder
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(15)
                .Rotate(Angle.HalfPi)
                .Move(80)
                .Stand(1)
                .AssertScores(0)
                .AssertLocation(150 - 30, 30, 10)
                .CreateTest();
        }
    }
}
