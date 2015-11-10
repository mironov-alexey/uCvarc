using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    partial class TBBLogicPartHelper
    {
        public void LoadDoorTest(LogicPart logic, TBBRules rules)
        {
            var builder = new TBBTestBuilder(rules, new TBBWorldState(0));

            logic.Tests["DoorClosing_CloseSuccess-IfNearby"] = builder
                .Commands
                    .Move(10)
                    .Rotate(Angle.HalfPi)
                    .Move(70)
                    .Stand(1)
                    .CloseDoor()
                .Back
                    .AssertScores(10)                    
                    .CreateTest();

            logic.Tests["DoorClosing_CloseFail-IfFar"] = builder
                .Commands
                    .Move(10)
                    .Rotate(Angle.HalfPi)
                    .Move(65)
                    .Stand(1)
                    .CloseDoor()
                .Back
                    .AssertScores(0)
                    .CreateTest();

            logic.Tests["DoorClosing_CloseFail-IfWrongAngle"] = builder
                .Commands
                    .Move(10)
                    .Rotate(Angle.HalfPi)
                    .Move(70)
                    .Rotate(Angle.FromGrad(10))
                    .Stand(1)
                    .CloseDoor()
                .Back
                    .AssertScores(0)
                    .CreateTest();

            logic.Tests["DoorClosing_CloseFail-IfBack"] = builder
                .Commands
                    .Move(10)
                    .Rotate(Angle.HalfPi)
                    .Move(70)
                    .Rotate(Angle.Pi)
                    .Stand(1)
                    .CloseDoor()
                .Back
                    .AssertScores(0)
                    .CreateTest();

            logic.Tests["DoorClosing_Penalty-IfWrongColor"] = builder                
                .Commands
                    .Rotate(Angle.HalfPi)
                    .Move(30)
                    .Rotate(-Angle.HalfPi)
                    .Move(230)
                    .Rotate(Angle.HalfPi)
                    .Move(40)
                    .Stand(1)
                    .CloseDoor()
                .Back
                    .AssertScores(-20)
                    .CreateTest();
        }
    }
}
