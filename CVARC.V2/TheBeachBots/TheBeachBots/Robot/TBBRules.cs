using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIRLab.Mathematics;
using CVARC.V2;

namespace TheBeachBots
{
    public class TBBRules : IRules, ISimpleMovementRules<TBBCommand>, IDoorOpeningRules<TBBCommand>, 
        ISeashellGripperRules<TBBCommand>, IFishingRules<TBBCommand>, ISandGripperRules<TBBCommand>
    {
        public static readonly TBBRules Current = new TBBRules();

        public double LinearVelocityLimit { get; set; }
        public Angle AngularVelocityLimit { get; set; }

        public double DoorInteractionRange { get; set; }
        public double DoorOpeningTime { get; set; }
        public double DoorClosingTime { get; set; }

        public double SeashellInteractionRange { get; set; }
        public double SeashellGrippingTime { get; set; }
        public double SeashellReleasingTime { get; set; }

        public double FishGrippingTime { get; set; }
        public double FishReleasingTime { get; set; }

        public double SandCollectingTime { get; set; }
        public double SandReleasingTime { get; set; }
        public int SandGripperCapacity { get; set; }

        public TBBRules()
        {
            LinearVelocityLimit = 50;
            AngularVelocityLimit = Angle.HalfPi;

            DoorOpeningTime = DoorClosingTime = 1;

            SeashellGrippingTime = SeashellReleasingTime = 1;

            DoorInteractionRange = SeashellInteractionRange = 10;

            FishGrippingTime = FishReleasingTime = 0.2;

            SandCollectingTime = SandReleasingTime = 1;
            SandGripperCapacity = 5;
        }

        public void DefineKeyboardControl(IKeyboardController _pool, string controllerId)
        {
            var pool = Compatibility.Check<KeyboardController<TBBCommand>>(this, _pool);
            this.AddSimpleMovementKeys(pool, controllerId);
            this.AddDoorUnitKeys(pool, controllerId);
            this.AddSeashellGripperKeys(pool, controllerId);
            this.AddFishingKeys(pool, controllerId);
            this.AddSandGripperKeys(pool, controllerId);
            pool.StopCommand = () => new TBBCommand { SimpleMovement = SimpleMovement.Stand(0.1) };
        }

    }
}
