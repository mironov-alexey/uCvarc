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
        public void LoadDoorTest(LogicPart logic, TBBRules rules)
        {
            var builder = new TBBTestBuilder(logic, rules, new TBBWorldState(0)) { Reflected = true };

            builder.CreateTest("DoorClosing_Close-IfNearby")
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(70)
                .Stand(1)
                .CloseDoor()
                .AssertScores(10)
                .EndOfTest();

            builder.CreateTest("DoorClosing_CloseFail-IfFar")
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(65)
                .Stand(1)
                .CloseDoor()
                .AssertScores(0)
                .EndOfTest();

            builder.CreateTest("DoorClosing_CloseFail-IfWrongAngle")
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(70)
                .Rotate(Angle.FromGrad(10))
                .Stand(1)
                .CloseDoor()
                .AssertScores(0)
                .EndOfTest();

            builder.CreateTest("DoorClosing_CloseFail-IfBack")
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(70)
                .Rotate(Angle.Pi)
                .Stand(1)
                .CloseDoor()
                .AssertScores(0)
                .EndOfTest();

            builder.CreateTest("DoorClosing_Penalty-IfWrongColor")                
                .Rotate(Angle.HalfPi)
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(230)
                .Rotate(Angle.HalfPi)
                .Move(40)
                .Stand(1)
                .CloseDoor()
                .AssertScores(-20)
                .EndOfTest();
        }
    }
}
