using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;

namespace TheBeachBots
{
    delegate void Assert(TBBSensorsData data, IAsserter asserter);

    class TBBTestBase : SelfReflectingCvarcTest<TBBSensorsData, TBBCommand, TBBWorld, TBBWorldState>
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
        Assert assert;

        public override TBBWorldState GetWorldState()
        {
            return worldState;
        }

        public TBBTestBase(TBBWorldState state, Assert assert, params TBBCommand[] test)
            : base(test)
        {
            this.assert = assert;
            worldState = state;
        }

        public TBBTestBase(Assert assert, params TBBCommand[] test)
            : this(new TBBWorldState(0), assert, test) { }

        protected override void Test(CvarcClient<TBBSensorsData, TBBCommand> client, TBBWorld world, 
            IAsserter asserter, IEnumerable<TBBCommand> test)
        {
            var data = new TBBSensorsData();
            foreach (var command in test)
                data = client.Act(command);
            this.assert(data, asserter);
        }
    }
}
