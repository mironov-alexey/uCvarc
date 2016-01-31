using AIRLab.Mathematics;
using CVARC.V2;

namespace TheBeachBots
{
    public class FishingTest : DefaultTBBTest
    {
        [CvarcTestMethod]
        public void Fishing_GripSuccess()
        {
            Robot
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Stand(1)
                .GripFish();
            AssertTrue(s => s.FishAttached);
        }

        [CvarcTestMethod]
        public void Fishing_GripFailIfFar()
        {
            Robot
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(90)
                .Stand(1)
                .GripFish();
            AssertFalse(s => s.FishAttached);
        }

        [CvarcTestMethod]
        public void Fishing_GripFailIfWrongAngle()
        {
            Robot
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Rotate(Angle.HalfPi)
                .Stand(1)
                .GripFish();
            AssertFalse(s => s.FishAttached);
        }

        [CvarcTestMethod]
        public void Fishing_GotScoresIfFishGripped()
        {
            Robot
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Stand(1)
                .GripFish();
            AssertEqual(s => s.SelfScores, 10);
        }

        [CvarcTestMethod]
        public void Fishing_GotScoresIfFishOutsideWater()
        {
            Robot
                .Move(50)
                .Rotate(-Angle.HalfPi)
                .Move(100)
                .Stand(1)
                .GripFish()
                .Move(-50)
                .Stand(1)
                .ReleaseFish();
            AssertEqual(s => s.SelfScores, 15);
        }

        [CvarcTestMethod]
        public void Fishing_GotScoresIfFishInNet()
        {
            Robot
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
                .ReleaseFish();
            AssertEqual(s => s.SelfScores, 20);
        }
    }
}
