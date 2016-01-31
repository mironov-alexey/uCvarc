using AIRLab.Mathematics;
using CVARC.V2;

namespace TheBeachBots
{
    public class TBBRules : IRules, ISimpleMovementRules<TBBCommand>, IDoorOpeningRules<TBBCommand>, 
        ISeashellGripperRules<TBBCommand>, IFishingRules<TBBCommand>, ISandGripperRules<TBBCommand>,
        IParasolRules<TBBCommand>
    {
        public static readonly TBBRules Current = new TBBRules();

        public double LinearVelocityLimit => 50;
        public Angle AngularVelocityLimit => Angle.HalfPi;

        public double DoorInteractionRange => 12;
        public double DoorOpeningTime => 1;
        public double DoorClosingTime => 1;

        public double SeashellInteractionRange => 12;
        public double SeashellGrippingTime => 1;
        public double SeashellReleasingTime => 1;

        public double FishInteractionRange => 12;
        public double FishGrippingTime => 1;
        public double FishReleasingTime => 1;

        public double SandInteractionRange => 12;
        public double SandCollectingTime => 1;
        public double SandReleasingTime => 1;
        public int SandGripperCapacity => 5;

        public double ParasolOpeningTime => 3;
        public double ParasolClosingTime => 3;

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
