using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots
{
    abstract class SelfReflectingCvarcTest<TSensorData, TCommand, TWorld, TWorldState> 
        : CvarcTest<TSensorData, TCommand, TWorld, TWorldState>
        where TCommand : ISimpleMovementCommand
        where TWorldState : IWorldState
        where TSensorData : class
    {
        private TCommand[] test;
        public bool Reflected { get; set; }

        public SelfReflectingCvarcTest(IEnumerable<TCommand> test)
            : base()
        {
            this.test = test.ToArray();
        } 

        private TCommand ReflectCommand(TCommand command)
        {
            command.SimpleMovement = SimpleMovement.MoveAndRotate(
                command.SimpleMovement.LinearVelocity,
                -command.SimpleMovement.AngularVelocity,
                command.SimpleMovement.Duration);
            return command;
        }

        public override void Test(CvarcClient<TSensorData, TCommand> client, TWorld world, IAsserter asserter)
        {
            Test(client, world, asserter, test.Select(x => Reflected ? ReflectCommand(x) : x));
        }

        protected abstract void Test(CvarcClient<TSensorData, TCommand> client, 
            TWorld world, IAsserter asserter, IEnumerable<TCommand> test);
    }
}
