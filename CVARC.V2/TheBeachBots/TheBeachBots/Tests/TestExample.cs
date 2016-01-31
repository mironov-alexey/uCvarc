using System.Collections.Generic;
using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{    
    public class TestExample : CvarcTestCase<TBBRules, TBBSensorsData, TBBCommand, TBBWorldState, TBBWorld>
    {
        public TestExample() : base(TBBRules.Current, new TBBWorldState(42), new SettingsProposal())
        {
            DefaultSettings.OperationalTimeLimit = 5;
            DefaultSettings.TimeLimit = 90;
            DefaultSettings.Controllers = new List<ControllerSettings>
            {
                new ControllerSettings {ControllerId = TwoPlayersId.Left, Name = "This", Type = ControllerType.Client},
                new ControllerSettings {ControllerId = TwoPlayersId.Right, Name = "Standing", Type = ControllerType.Bot}
            };
        }

        [CvarcTestMethod]
        public void SimpleTest()
        {
            Robot
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(15)
                .Rotate(Angle.HalfPi)
                .Move(80)
                .Stand(1);
            AssertEqual(s => s.SelfScores, 0, 0);
        }

        [CvarcTestMethod]
        [TestSettings.SetReflected(true)]
        public void ReflectedTest()
        {
            Robot
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(15)
                .Rotate(Angle.HalfPi)
                .Move(80)
                .Stand(1);
            AssertEqual(s => s.SelfScores, 0, 0);
        }

        [CvarcTestMethod]
        [TestSettings.SpeedUp(true)]
        [TestSettings.TimeLimit(90)]
        public void SpeedUpTest()
        {
            Robot
                .Move(30)
                .Rotate(-Angle.HalfPi)
                .Move(15)
                .Rotate(Angle.HalfPi)
                .Move(80)
                .Stand(1);
            AssertEqual(s => s.SelfScores, 0, 0);
        }
    }
}
