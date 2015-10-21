using AIRLab.Mathematics;
using CVARC.V2;

namespace TheBeachBots
{
    partial class TBBLogicPartHelper
    {
        public void LoadFishingTest(LogicPart logic, TBBRules rules)
        {
            var builder = new TBBTestBuilder(rules, new TBBWorldState(0));

            logic.Tests["Fishing_GripSuccess-IfNearby"] = builder
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Stand(1)
                .GripFish()
                .AssertHasFish(true)
                .CreateTest();

            logic.Tests["Fishing_GripFail-IfFar"] = builder
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(90)
                .Stand(1)
                .GripFish()
                .AssertHasFish(false)
                .CreateTest();

            logic.Tests["Fishing_GripFail-IfWrongAngle"] = builder
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Rotate(Angle.FromGrad(10))
                .Stand(1)
                .GripFish()
                .AssertHasFish(false)
                .CreateTest();

            logic.Tests["Fishing_GotScores-IfFishGripped"] = builder
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Stand(1)
                .GripFish()
                .AssertScores(5)
                .CreateTest();

            logic.Tests["Fishing_GotScores-IfFishOutsideWater"] = builder
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Stand(1)
                .GripFish()
                .Move(-50)
                .Stand(1)
                .ReleaseFish()
                .AssertScores(5)
                .CreateTest();

            logic.Tests["Fishing_GotScores-IfFishInNet"] = builder
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
                .CreateTest();
        }
    }
}
