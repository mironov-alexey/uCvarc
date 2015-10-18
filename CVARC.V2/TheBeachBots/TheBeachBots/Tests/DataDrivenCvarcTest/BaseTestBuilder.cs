using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVARC.V2
{
    public abstract class BaseTestBuilder<TSensorData, TCommand, TWorldState, TWorld>
        where TWorld : IWorld
        where TCommand : ICommand
        where TWorldState : IWorldState
        where TSensorData : class, new()
    {        
        LogicPart logic;
        TestData<TSensorData, TCommand, TWorld, TWorldState> data;
        
        protected SettingsProposal Settings { get; private set; }

        public TWorldState WorldState { get; set; }

        public double? OperationalTimeLimit
        {
            get { return Settings.OperationalTimeLimit; }
            set { Settings.OperationalTimeLimit = value; }
        }

        public double? TimeLimit
        {
            get { return Settings.TimeLimit; }
            set { Settings.TimeLimit = value; }
        }

        public string LogFile
        {
            get { return Settings.LogFile; }
            set { Settings.LogFile = value; }
        }

        public bool? EnableLog
        {
            get { return Settings.EnableLog; }
            set { Settings.EnableLog = value; }
        }

        public bool? SpeedUp
        {
            get { return Settings.SpeedUp; }
            set { Settings.SpeedUp = value; }
        }

        public BaseTestBuilder(LogicPart logic, TWorldState worldState)
        {
            this.logic = logic;
            WorldState = worldState;            
            Settings = new SettingsProposal();
            Settings.Controllers = new List<ControllerSettings>();
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

        public void CreateClearData(string testName)
        {
            data = new TestData<TSensorData, TCommand, TWorld, TWorldState>
                (testName, WorldState, new SettingsProposal(Settings));
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
