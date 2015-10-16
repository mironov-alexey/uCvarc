using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVARC.V2
{
    public abstract class BaseTestBuilder<TSensorData, TCommand, TWorldState, TWorld, TRules>
        where TRules : IRules
        where TWorld : IWorld
        where TCommand : ICommand
        where TWorldState : IWorldState
        where TSensorData : class, new()
    {        
        LogicPart logic;
        TestData<TSensorData, TCommand, TWorld, TWorldState> data;

        protected readonly TRules Rules;
        protected readonly SettingsProposal settings;

        public TWorldState WorldState { get; set; }

        public double? OperationalTimeLimit
        {
            get { return settings.OperationalTimeLimit; }
            set { settings.OperationalTimeLimit = value; }
        }

        public double? TimeLimit
        {
            get { return settings.TimeLimit; }
            set { settings.TimeLimit = value; }
        }

        public string LogFile
        {
            get { return settings.LogFile; }
            set { settings.LogFile = value; }
        }

        public bool? EnableLog
        {
            get { return settings.EnableLog; }
            set { settings.EnableLog = value; }
        }

        public bool? SpeedUp
        {
            get { return settings.SpeedUp; }
            set { settings.SpeedUp = value; }
        }

        public BaseTestBuilder(LogicPart logic, TRules rules, TWorldState worldState)
        {
            this.WorldState = worldState;
            this.logic = logic;
            this.Rules = rules;
            settings = new SettingsProposal();
            settings.Controllers = new List<ControllerSettings>();
        }

        public void AddControllerSettings(string controllerId, string name, ControllerType type)
        {
            settings.Controllers.Add(new ControllerSettings
            {
                ControllerId = controllerId,
                Name = name,
                Type = type,
            });
        }

        public void CreateClearData(string testName)
        {
            // FIXME: все поля в дата передаются по ссылке, поэтому изменение
            // параметров билдера ведет к изменению параметров всех тестов

            data = new TestData<TSensorData, TCommand, TWorld, TWorldState>()
            {
                Settings = this.settings,
                WorldState = this.WorldState,
                TestName = testName,
            };
        }

        public void EndOfTest()
        {
            logic.Tests[data.TestName] = new DataDrivenCvarcTest<TSensorData, TCommand, TWorld, TWorldState>(data);
        }

        protected virtual void AddAction(TCommand command)
        {
            data.AddAction(command);
        }

        protected virtual void AddAction(Asserter<TSensorData, TWorld> assert)
        {
            data.AddAction(assert);
        }
    }
}
