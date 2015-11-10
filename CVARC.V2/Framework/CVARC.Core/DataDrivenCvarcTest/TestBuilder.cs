using System;
using System.Collections.Generic;

namespace CVARC.V2
{   
    public class TestBuilder<TRules, TSensorData, TCommand, TWorldState, TWorld>
        where TRules : IRules
        where TSensorData : class, new()
        where TCommand : class, ICommand
        where TWorldState : IWorldState
        where TWorld : IWorld
    {
        public SettingsProposal Settings { get; }
        public TWorldState WorldState { get; set; }
        public readonly TRules Rules;

        List<ITestAction<TSensorData, TCommand>> currentTest;

        public TestBuilder(TRules rules, TWorldState worldState, SettingsProposal settings)
        {
            Rules = rules;
            WorldState = worldState;            
            Settings = settings;
            currentTest = new List<ITestAction<TSensorData, TCommand>>();
        }

        public void AddControllerSettings(string controllerId, string name, ControllerType type)
        {
            Settings.Controllers.Add(new ControllerSettings
            {
                ControllerId = controllerId,
                Name = name,
                Type = type,
            });
        }

        public virtual CvarcTest<TSensorData, TCommand, TWorld, TWorldState> CreateTest()
        {
            var data = new TestData<TSensorData, TCommand, TWorldState>
                (WorldState, SettingsProposal.DeepCopy(Settings), currentTest);

            currentTest = new List<ITestAction<TSensorData, TCommand>>();

            return new DataDrivenCvarcTest<TSensorData, TCommand, TWorld, TWorldState>(data);
        }

        protected virtual void AddCommand(TCommand command)
        {
            currentTest.Add(new TestAction<TSensorData, TCommand>(command));
        }

        protected virtual void AddAssert(ISensorAsserter<TSensorData> assert)
        {
            currentTest.Add(new TestAction<TSensorData, TCommand>(assert));
        }
       
        protected virtual void AddAssert(Action<TSensorData, IAsserter> assert)
        {
            currentTest.Add(new TestAction<TSensorData, TCommand>(new DelegatedAsserter<TSensorData>(assert)));
        }
    }
}
