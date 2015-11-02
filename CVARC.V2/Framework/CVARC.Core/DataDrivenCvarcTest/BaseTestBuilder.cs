using System;
using System.Collections.Generic;

namespace CVARC.V2
{
    [Obsolete]
    public abstract class BaseTestBuilder<TSensorData, TCommand, TWorldState, TWorld>
        where TWorld : IWorld
        where TCommand : ICommand
        where TWorldState : IWorldState
        where TSensorData : class, new()
    {
        List<TestAction<TSensorData, TCommand>> currentTest = 
            new List<TestAction<TSensorData, TCommand>>();        

        public SettingsProposal Settings { get; private set; }
        public TWorldState WorldState { get; set; }

        public BaseTestBuilder(TWorldState worldState)
            : this(worldState, new SettingsProposal() { Controllers = new List<ControllerSettings>() })
        { }

        public BaseTestBuilder(TWorldState worldState, SettingsProposal settings)
        {
            WorldState = worldState;            
            Settings = settings;
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

        public CvarcTest<TSensorData, TCommand, TWorld, TWorldState> CreateTest()
        {
            var data = new TestData<TSensorData, TCommand, TWorld, TWorldState>
                (WorldState, SettingsProposal.DeepCopy(Settings), currentTest);

            currentTest = new List<TestAction<TSensorData, TCommand>>();

            return new DataDrivenCvarcTest<TSensorData, TCommand, TWorld, TWorldState>(data);
        }

        protected virtual void AddTestAction(TCommand command)
        {
            currentTest.Add(new TestAction<TSensorData, TCommand> { Command = command });
        }

        protected virtual void AddTestAction(Asserter<TSensorData> assert)
        {
            currentTest.Add(new TestAction<TSensorData, TCommand> { Asserter = assert });
        }
    }
}
