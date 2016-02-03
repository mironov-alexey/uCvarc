using AIRLab.Mathematics;
using CVARC.V2;

namespace TheBeachBots
{
    public class TBBRules : IRules, ISimpleMovementRules<TBBCommand>, IDoorOpeningRules<TBBCommand>, 
        ISeashellGripperRules<TBBCommand>, IFishingRules<TBBCommand>, ISandGripperRules<TBBCommand>,
        IParasolRules<TBBCommand>
    {
        public static readonly TBBRules Current = new TBBRules();

        public double LinearVelocityLimit { get { return 50; } }
        public Angle AngularVelocityLimit { get { return Angle.HalfPi; } }

        public double DoorInteractionRange { get { return 12; } }
        public double DoorOpeningTime { get { return 1; } }
        public double DoorClosingTime { get { return 1; } }

        public double SeashellInteractionRange { get { return 10; } }
        public double SeashellGrippingTime { get { return 1; } }
        public double SeashellReleasingTime { get { return 1; } }

        public double FishInteractionRange { get { return 12; } }
        public double FishGrippingTime { get { return 1; } }
        public double FishReleasingTime { get { return 1; } }

        public double SandInteractionRange { get { return 12; } }
        public double SandCollectingTime { get { return 1; } }
        public double SandReleasingTime { get { return 1; } }
        public int SandGripperCapacity { get { return 5; } }

        public double ParasolOpeningTime { get { return 3; } }
        public double ParasolClosingTime { get { return 3; } }

        public void DefineKeyboardControl(IKeyboardController _pool, string controllerId)
        {
            var pool = Compatibility.Check<KeyboardController<TBBCommand>>(this, _pool);
            this.AddSimpleMovementKeys(pool, controllerId);
            this.AddDoorUnitKeys(pool, controllerId);
            this.AddSeashellGripperKeys(pool, controllerId);
            this.AddFishingKeys(pool, controllerId);
            this.AddSandGripperKeys(pool, controllerId);
            this.AddParasolUnitKeys(pool, controllerId);
            pool.StopCommand = () => new TBBCommand { SimpleMovement = SimpleMovement.Stand(0.1) };
        }

    }
}
