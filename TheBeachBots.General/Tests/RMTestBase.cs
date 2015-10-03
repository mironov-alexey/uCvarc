using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;

namespace TheBeachBots
{
    public delegate void TBBTestEntry(CvarcClient<TBBSensorsData, TBBCommand> client, TBBWorld world, IAsserter asserter);

    public class RMTestBase : DelegatedCvarcTest<TBBSensorsData, TBBCommand, TBBWorld, TBBWorldState>
    {
        public override SettingsProposal GetSettings()
        {
            return new SettingsProposal
            {
                TimeLimit = 30,
                Controllers = new List<ControllerSettings> 
                    {
                        new ControllerSettings  { ControllerId=TwoPlayersId.Left, Name="This", Type= ControllerType.Client},
                        new ControllerSettings  { ControllerId=TwoPlayersId.Right, Name="Standing", Type= ControllerType.Bot}
                    }
            };
        }

        TBBWorldState worldState;

        public override TBBWorldState GetWorldState()
        {
            return worldState;
        }

        public RMTestBase(TBBTestEntry entry, TBBWorldState state)
            : base((client, world, asserter) => { entry(client, world, asserter); })
        {
            worldState = state;
        }
    }
}
