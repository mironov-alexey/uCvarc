using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIRLab.Mathematics;
using CVARC.V2;

namespace TheBeachBots
{
    class TBBRules : IRules, ISimpleMovementRules<TBBCommand>
    {
        public static readonly TBBRules Current = new TBBRules();

        public double LinearVelocityLimit { get; set; }
        public Angle AngularVelocityLimit { get; set; }

        public TBBRules()
        {
            LinearVelocityLimit = 50;
            AngularVelocityLimit = Angle.HalfPi;
        }

        public void DefineKeyboardControl(IKeyboardController _pool, string controllerId)
        {
            var pool = Compatibility.Check<KeyboardController<TBBCommand>>(this, _pool);
            this.AddSimpleMovementKeys<TBBCommand>(pool, controllerId);
            pool.StopCommand = () => new TBBCommand { SimpleMovement = SimpleMovement.Stand(0.1) };
        }
    }
}
