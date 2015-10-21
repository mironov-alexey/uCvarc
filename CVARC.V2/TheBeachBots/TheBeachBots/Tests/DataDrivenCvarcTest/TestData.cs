using System.Collections.Generic;

namespace CVARC.V2
{
    public class TestData<TSensorData, TCommand, TWorld, TWorldState>
        where TWorldState : IWorldState
        where TCommand : ICommand
        where TWorld : IWorld
    {
        public string TestName { get; private set; }
        public TWorldState WorldState { get; private set; }
        public SettingsProposal Settings { get; private set; }

        protected readonly List<TestAction<TSensorData, TCommand, TWorld>> actions;

        public IEnumerable<TestAction<TSensorData, TCommand, TWorld>> Actions { get { return this.actions; } }

        public TestData(string testName, TWorldState worldState, SettingsProposal settings)
        {
            TestName = testName;
            WorldState = worldState;
            Settings = settings;
            actions = new List<TestAction<TSensorData, TCommand, TWorld>>(); 
        }

        public void AddAction(TCommand command)
        {
            actions.Add(new TestAction<TSensorData, TCommand, TWorld> { Command = command });
        }

        public void AddAction(Asserter<TSensorData, TWorld> assert)
        {
            actions.Add(new TestAction<TSensorData, TCommand, TWorld> { Asserter = assert });
        }
    }
}
