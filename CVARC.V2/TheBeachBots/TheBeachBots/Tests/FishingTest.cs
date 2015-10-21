using AIRLab.Mathematics;
using CVARC.V2;

namespace TheBeachBots
{
    partial class TBBLogicPartHelper
    {
        [TestLoaderMethod]
        public void LoadFishingTest(LogicPart logic, TBBRules rules)
        {
            var builder = new TBBTestBuilder(logic, rules, new TBBWorldState(0));

            builder.CreateTest("Fishing_GripSuccess-IfNearby")
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Stand(1)
                .GripFish()
                .AssertHasFish(true)
                .EndOfTest();

            builder.CreateTest("Fishing_GripFail-IfFar")
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(90)
                .Stand(1)
                .GripFish()
                .AssertHasFish(false)
                .EndOfTest();

            builder.CreateTest("Fishing_GripFail-IfWrongAngle")
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Rotate(Angle.FromGrad(10))
                .Stand(1)
                .GripFish()
                .AssertHasFish(false)
                .EndOfTest();

            builder.CreateTest("Fishing_GotScores-IfFishGripped")
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Stand(1)
                .GripFish()
                .AssertScores(5)
                .EndOfTest();

            builder.CreateTest("Fishing_GotScores-IfFishOutsideWater")
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Stand(1)
                .GripFish()
                .Move(-50)
                .Stand(1)
                .ReleaseFish()
                .AssertScores(5)
                .EndOfTest();

            builder.CreateTest("Fishing_GotScores-IfFishInNet")
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Stand(1)
                .GripFish()
                .Move(-20)
                .Rotate(Angle.HalfPi)
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(20)
                .Stand(1)
                .ReleaseFish()
                .AssertScores(10)
                .EndOfTest();
        }
    }
}
