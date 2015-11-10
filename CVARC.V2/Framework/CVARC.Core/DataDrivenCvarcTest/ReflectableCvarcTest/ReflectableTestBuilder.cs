using System;
using System.Collections.Generic;
using System.Linq;

namespace CVARC.V2
{
    public abstract class ReflectableTestBuilder<TRules, TSensorData, TCommand, TWorldState, TWorld>
        : TestBuilder<TRules, TSensorData, TCommand, TWorldState, TWorld>
        where TWorld : IWorld
        where TRules : IRules
        where TCommand : class, ISimpleMovementCommand
        where TWorldState : IWorldState
        where TSensorData : class, new()
    {
        public bool Reflected { get; set; }

        private List<ReflectableTestAction<TSensorData, TCommand>> currentTest; 

        protected ReflectableTestBuilder(TRules rules, TWorldState worldState, SettingsProposal settings)
            : base(rules, worldState, settings)
        {
            currentTest = new List<ReflectableTestAction<TSensorData, TCommand>>();
        }

        protected override void AddCommand(TCommand command)
        {
            currentTest.Add(new ReflectableTestAction<TSensorData, TCommand>(command));
        }

        protected override void AddAssert(ISensorAsserter<TSensorData> assert)
        {
            currentTest.Add(new ReflectableTestAction<TSensorData, TCommand>(assert));
        }

        protected override void AddAssert(Action<TSensorData, IAsserter> assert)
        {
            currentTest.Add(new ReflectableTestAction<TSensorData, TCommand>(new DelegatedAsserter<TSensorData>(assert)));
        }

        public override CvarcTest<TSensorData, TCommand, TWorld, TWorldState> CreateTest()
        {
            var newSettings = SettingsProposal.DeepCopy(Settings);

            if (Reflected)
            {
                currentTest.ForEach(act => act.Reflect());
                newSettings.Controllers = Reflect(newSettings.Controllers);
            }

            var baseTest = currentTest.Select(x => x as ITestAction<TSensorData, TCommand>);           
            var data = new TestData<TSensorData, TCommand, TWorldState>(WorldState, newSettings, baseTest);

            currentTest = new List<ReflectableTestAction<TSensorData, TCommand>>();

            return new DataDrivenCvarcTest<TSensorData, TCommand, TWorld, TWorldState>(data);
        }
        
        private List<ControllerSettings> Reflect(List<ControllerSettings> controllers)
        {
            return controllers
                .Select(x => new ControllerSettings()
                {
                    ControllerId = ReflectControllerId(x.ControllerId),
                    Name = x.Name,
                    Type = x.Type,
                })
                .ToList();
        }

        private string ReflectControllerId(string id)
        {
            if (id == TwoPlayersId.Left)
                return TwoPlayersId.Right;
            if (id == TwoPlayersId.Right)
                return TwoPlayersId.Left;
            throw new ArgumentException("Invalid two players id!");                    
        }
    }
}
