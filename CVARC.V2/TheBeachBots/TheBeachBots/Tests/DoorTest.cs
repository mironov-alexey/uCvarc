using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    public class DoorTest : DefaultTBBTest
    {
        [CvarcTestMethod]
        public void DoorClosing_CloseSuccess()
        {
            Robot
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(70)
                .Stand(1)
                .CloseDoor()
                .Stand(1);
            AssertEqual(s => s.SelfScores, 10);
        }

        [CvarcTestMethod]
        public void DoorClosing_CloseFailsIfFar()
        {
            Robot
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(65)
                .Stand(1)
                .CloseDoor();
            AssertEqual(s => s.SelfScores, 0);
        }

        [CvarcTestMethod]
        public void DoorClosing_CloseFailsIfWrongAngle()
        {
            Robot
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(70)
                .Rotate(Angle.FromGrad(10))
                .Stand(1)
                .CloseDoor();
            AssertEqual(s => s.SelfScores, 0);
        }

        [CvarcTestMethod]
        public void DoorClosing_CloseFailsIfBack()
        {
            Robot
                .Move(10)
                .Rotate(Angle.HalfPi)
                .Move(70)
                .Rotate(Angle.Pi)
                .Stand(1)
                .CloseDoor();
            AssertEqual(s => s.SelfScores, 0);
        }

        [CvarcTestMethod]
        public void DoorClosing_PenaltyIfCloseOpponentsDoor()
        {
            Robot
                .Rotate(Angle.HalfPi)
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(230)
                .Rotate(Angle.HalfPi)
                .Move(40)
                .Stand(1)
                .CloseDoor();
            AssertEqual(s => s.SelfScores, -20);
        }
    }
}
