using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    partial class TBBLogicPartHelper
    {
        public void LoadDoorTest(LogicPart logic, TBBRules rules)
        {
            var builder = new TBBTestBuilder(rules, new TBBWorldState(0));

            logic.Tests["DoorClosing_Close-IfNearby"] = builder
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(70)
                .Stand(1)
                .CloseDoor()
                .AssertScores(10)
                .CreateTest();

            logic.Tests["DoorClosing_CloseFail-IfFar"] = builder
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(65)
                .Stand(1)
                .CloseDoor()
                .AssertScores(0)
                .CreateTest();

            logic.Tests["DoorClosing_CloseFail-IfWrongAngle"] = builder
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(70)
                .Rotate(Angle.FromGrad(10))
                .Stand(1)
                .CloseDoor()
                .AssertScores(0)
                .CreateTest();

            logic.Tests["DoorClosing_CloseFail-IfBack"] = builder
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(70)
                .Rotate(Angle.Pi)
                .Stand(1)
                .CloseDoor()
                .AssertScores(0)
                .CreateTest();

            logic.Tests["DoorClosing_Penalty-IfWrongColor"] = builder                
                .Rotate(Angle.HalfPi)
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(230)
                .Rotate(Angle.HalfPi)
                .Move(40)
                .Stand(1)
                .CloseDoor()
                .AssertScores(-20)
                .CreateTest();
        }
    }
}
