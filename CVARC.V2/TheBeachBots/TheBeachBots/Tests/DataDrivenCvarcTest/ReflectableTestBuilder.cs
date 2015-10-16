using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVARC.V2
{
    public abstract class ReflectableTestBuilder<TSensorData, TCommand, TWorldState, TWorld, TRules>
        : BaseTestBuilder<TSensorData, TCommand, TWorldState, TWorld, TRules>
        where TWorld : IWorld
        where TRules : IRules
        where TCommand : ISimpleMovementCommand
        where TWorldState : IWorldState
        where TSensorData : class, ILocationSensorData, new()
    {
        public bool IsReflected { get; private set; }

        public void Reflect()
        {
            IsReflected = !IsReflected;
            ReflectControllerSetup();
        }

        public ReflectableTestBuilder(LogicPart logic, TRules rules, TWorldState worldState)
            : base(logic, rules, worldState) { }

        protected override void AddAction(TCommand command)
        {
            base.AddAction(IsReflected ? ReflectCommand(command) : command);
        }

        protected override void AddAction(Asserter<TSensorData, TWorld> assert)
        {
            base.AddAction(IsReflected ? (s, w, a) => assert(ReflectSensor(s), w, a) : assert);
        }
        
        private void ReflectControllerSetup()
        {
            foreach (var controller in settings.Controllers)
                controller.ControllerId = ReflectControllerId(controller.ControllerId);
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
            command.SimpleMovement = SimpleMovement.MoveAndRotate(
                command.SimpleMovement.LinearVelocity,
                -command.SimpleMovement.AngularVelocity,
                command.SimpleMovement.Duration);
            return command;
        }

        private TSensorData ReflectSensor(TSensorData sensor)
        {
            // TODO: apply some math to reflect robot angle and location
            sensor.SelfLocation = new LocatorItem
            {
                Id = sensor.SelfLocation.Id,              
                X = sensor.SelfLocation.X,
                Y = sensor.SelfLocation.Y,
                Angle = sensor.SelfLocation.Angle,
            };

            throw new NotImplementedException();

            return sensor;
        }
    }
}
