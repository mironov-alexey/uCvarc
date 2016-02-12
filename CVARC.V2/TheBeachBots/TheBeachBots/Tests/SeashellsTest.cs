using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    public class SeashellsTest : DefaultTBBTest
    {
        [CvarcTestMethod]
        public void Seashells_GripOnGroundSuccess()
        {
            Robot
                .Rotate(-Angle.HalfPi)
                .Move(20)
                .Stand(1)
                .GripSeashell();

            AssertTrue(s => s.SeashellAttached);
        }

        [CvarcTestMethod]
        public void Seashells_ReleaseWorking()
        {
            Robot
                .Rotate(-Angle.HalfPi)
                .Move(20)
                .Stand(1)
                .GripSeashell();

            AssertTrue(s => s.SeashellAttached);

            Robot.ReleaseSeashell();

            AssertFalse(s => s.SeashellAttached);
        }

        [CvarcTestMethod]
        public void Seashells_GripOnGroundFailsIfFar()
        {
            Robot
                .Rotate(-Angle.HalfPi)
                .Move(12)
                .Stand(1)
                .GripSeashell();

            AssertFalse(s => s.SeashellAttached);
        }

        [CvarcTestMethod]
        public void Seashells_GripOnGroundFailsIfWrongAngle()
        {
            Robot
                .Rotate(-Angle.HalfPi)
                .Move(20)
                .Rotate(Angle.Pi / 4)
                .Stand(1)
                .GripSeashell();

            AssertFalse(s => s.SeashellAttached);
        }

        [CvarcTestMethod]
        public void Seashells_GripOnRocksSuccess()
        {
            Robot                
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(75)
                .Rotate(-Angle.HalfPi)
                .Move(30)
                .Rotate(Angle.HalfPi)
                .Stand(1)
                .GripSeashell();

            AssertTrue(s => s.SeashellAttached);
        }

        [CvarcTestMethod]
        public void Seashells_GotTwoScoresIfRightColorPlacedOnTowel()
        {
            Robot
                .Rotate(-Angle.HalfPi)
                .Move(20)
                .Stand(1)
                .GripSeashell()
                .Move(-35)
                .Stand(1)
                .ReleaseSeashell();

            AssertEqual(s => s.SelfScores, 2);
        }

        [CvarcTestMethod]
        public void Seashells_GotTwoScoresIfWhitePlacedOnTowel()
        {
            Robot
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(70)
                .Rotate(-Angle.HalfPi)
                .Move(5)
                .Stand(1)
                .GripSeashell()
                .Move(-5)
                .Rotate(-Angle.HalfPi)
                .Move(70)
                .Rotate(Angle.HalfPi)
                .Move(10)
                .Stand(1)
                .ReleaseSeashell();

            AssertEqual(s => s.SelfScores, 2);
        }

        [CvarcTestMethod]
        public void Seashells_GotPenaltyIfWrongColorGripped()
        {
            Robot
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(95)
                .Rotate(Angle.HalfPi)
                .Move(135)
                .Rotate(Angle.HalfPi)
                .Stand(1)
                .GripSeashell();

            AssertEqual(s => s.SelfScores, -20);
        }

        [CvarcTestMethod]
        public void Seashells_GotZeroScoresIfWrongColorPlacedOnTowel()
        {
            Robot
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(95)
                .Rotate(Angle.HalfPi)
                .Move(135)
                .Rotate(Angle.HalfPi)
                .Stand(1)
                .GripSeashell()
                .Rotate(Angle.HalfPi)
                .Move(135)
                .Rotate(-Angle.HalfPi)
                .Move(95)
                .Rotate(Angle.HalfPi)
                .Move(5)
                .Stand(1)
                .ReleaseSeashell();

            AssertEqual(s => s.SelfScores, -20); // penalty for gripping
        }
    }
}
