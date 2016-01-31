using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;

namespace TheBeachBots
{
    public class DefaultTBBTest : CvarcTestCase<TBBRules, TBBSensorsData, TBBCommand, TBBWorldState, TBBWorld>
    {
        public DefaultTBBTest() : base(TBBRules.Current, new TBBWorldState(42), new SettingsProposal())
        {
            DefaultSettings.OperationalTimeLimit = 5;
            DefaultSettings.TimeLimit = 90;
            DefaultSettings.Controllers = new List<ControllerSettings>
            {
                new ControllerSettings {ControllerId = TwoPlayersId.Left, Name = "This", Type = ControllerType.Client},
                new ControllerSettings {ControllerId = TwoPlayersId.Right, Name = "Standing", Type = ControllerType.Bot}
            };
        }
    }
}