using AIRLab.Mathematics;

namespace CVARC.V2
{
    public class ReflectableTestAction<TSensorData, TCommand> 
        : TestAction<TSensorData, TCommand>, IReflectableTestAction<TSensorData, TCommand>
        where TCommand : ISimpleMovementCommand
    {
        public ReflectableTestAction(TCommand command) : base(command) { }
        public ReflectableTestAction(ISensorAsserter<TSensorData> asserter) : base(asserter) { } 

        public void Reflect()
        {
            if (Command != null) ReflectCommand(Command);
            if (Asserter != null) ReflectAsserter(Asserter);
        }

        private void ReflectCommand(TCommand command)
        {
            if (command.SimpleMovement == null) return;
            command.SimpleMovement = SimpleMovement.MoveAndRotate(
                command.SimpleMovement.LinearVelocity,
                -command.SimpleMovement.AngularVelocity,
                command.SimpleMovement.Duration);
        }

        private void ReflectAsserter(ISensorAsserter<TSensorData> asserter)
        {
            if (!(asserter is AbstractLocationAsserter<TSensorData>)) return;

            var locAsseter = (AbstractLocationAsserter<TSensorData>) asserter;
            locAsseter.ExpextedLocation.X = -locAsseter.ExpextedLocation.X;
            locAsseter.ExpextedLocation.Y = -locAsseter.ExpextedLocation.Y;
            locAsseter.ExpextedLocation.Angle = Angle.FromGrad(Angle.Pi.Grad - locAsseter.ExpextedLocation.Angle.Grad);            
        }
    }
}
