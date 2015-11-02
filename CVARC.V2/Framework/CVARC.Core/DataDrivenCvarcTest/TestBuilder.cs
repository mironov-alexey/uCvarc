﻿using System.Collections.Generic;

namespace CVARC.V2
{
   
    public class TestBuilder<TRules, TSensorData, TCommand, TWorldState, TWorld>
        where TRules : IRules
        where TWorld : IWorld
        where TCommand : ICommand
        where TWorldState : IWorldState
        where TSensorData : class, new()
    {
        List<TestAction<TSensorData, TCommand>> currentTest = 
            new List<TestAction<TSensorData, TCommand>>();        

        public SettingsProposal Settings { get; private set; }
        public TWorldState WorldState { get; set; }
        public readonly TRules Rules;
        public readonly CommandBuilder<TRules, TCommand> Builder;
        

        public TestBuilder(TRules rules, TWorldState worldState, SettingsProposal settings)
        {
            this.Rules = rules;
            this.Builder = new CommandBuilder<TRules, TCommand>(Rules);
            Builder.CommandAdded += z => AddTestAction(z);
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

        void AddTestAction(TCommand command)
        {
            currentTest.Add(new TestAction<TSensorData, TCommand> { Command = command });
        }

        protected void AddTestAction(Asserter<TSensorData> assert)
        {
            currentTest.Add(new TestAction<TSensorData, TCommand> { Asserter = assert });
        }
    }
}
