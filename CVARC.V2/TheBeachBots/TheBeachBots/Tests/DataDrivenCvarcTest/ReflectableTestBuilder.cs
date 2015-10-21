using System;
using System.Linq;
using AIRLab.Mathematics;

namespace CVARC.V2
{
    public abstract class ReflectableTestBuilder<TSensorData, TCommand, TWorldState, TWorld>
        : BaseTestBuilder<TSensorData, TCommand, TWorldState, TWorld>
        where TWorld : IWorld
        where TCommand : ISimpleMovementCommand
        where TWorldState : IWorldState
        where TSensorData : class, ILocationSensorData, new()
    {
        public bool Reflected { get; private set; }
        public void Reflect()
        {
            Reflected = !Reflected;
            ReflectControllerSettings();
        }

        public ReflectableTestBuilder(TWorldState worldState)
            : base(worldState) { }

        public ReflectableTestBuilder(TWorldState worldState, SettingsProposal settings)
            : base(worldState, settings) { }

        protected override void AddTestAction(TCommand command)
        {
            base.AddTestAction(Reflected ? ReflectCommand(command) : command);
        }

        protected override void AddTestAction(Asserter<TSensorData, TWorld> assert)
        {
            base.AddTestAction(Reflected ? (s, w, a) => assert(ReflectSensor(s), w, a) : assert);
        }
        
        private void ReflectControllerSettings()
        {
            Settings.Controllers = Settings.Controllers
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

        private TCommand ReflectCommand(TCommand command)
        {
            if (command.SimpleMovement == null) return command;
            command.SimpleMovement = SimpleMovement.MoveAndRotate(
                command.SimpleMovement.LinearVelocity,
                -command.SimpleMovement.AngularVelocity,
                command.SimpleMovement.Duration);
            return command;
        }

        private TSensorData ReflectSensor(TSensorData sensor)
        {
            if (sensor.SelfLocation == null) return sensor;
            sensor.SelfLocation = new LocatorItem
            {
                Id = sensor.SelfLocation.Id,              
                X = -sensor.SelfLocation.X,
                Y = sensor.SelfLocation.Y,
                Angle = Angle.Pi.Grad - sensor.SelfLocation.Angle,
            };
            return sensor;
        }
    }
}
