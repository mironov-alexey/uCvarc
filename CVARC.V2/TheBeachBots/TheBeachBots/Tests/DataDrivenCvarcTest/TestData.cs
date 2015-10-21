using System.Collections.Generic;
using System.Linq;

namespace CVARC.V2
{
    public class TestData<TSensorData, TCommand, TWorld, TWorldState>
        where TWorldState : IWorldState
        where TCommand : ICommand
        where TWorld : IWorld
    {
        public TWorldState WorldState { get; private set; }
        public SettingsProposal Settings { get; private set; }

        readonly List<TestAction<TSensorData, TCommand, TWorld>> actions;
        public IEnumerable<TestAction<TSensorData, TCommand, TWorld>> Actions { get { return actions; } }

        public TestData(TWorldState worldState, SettingsProposal settings, 
            IEnumerable<TestAction<TSensorData, TCommand, TWorld>> actions)
        {
            WorldState = worldState;
            Settings = settings;
            this.actions = actions.ToList();
        }
    }
}
