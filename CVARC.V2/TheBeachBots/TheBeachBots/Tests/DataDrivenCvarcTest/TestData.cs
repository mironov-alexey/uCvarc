using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVARC.V2
{
    public class TestData<TSensorData, TCommand, TWorld, TWorldState>
        where TWorldState : IWorldState
        where TCommand : ICommand
        where TWorld : IWorld
    {
        public string TestName { get; set; }
        public TWorldState WorldState { get; set; }
        public SettingsProposal Settings { get; set; }

        protected readonly List<TestAction<TSensorData, TCommand, TWorld>> actions;

        public IEnumerable<TestAction<TSensorData, TCommand, TWorld>> Actions { get { return this.actions; } }

        public TestData()
        {
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
