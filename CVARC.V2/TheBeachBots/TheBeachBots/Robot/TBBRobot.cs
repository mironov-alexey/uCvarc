using System.Collections.Generic;
using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    class TBBRobot : Robot<ITBBActorManager, TBBWorld, TBBSensorsData, TBBCommand, TBBRules>, ITBBRobot
    {
        public SimpleMovementUnit SimpleMovementUnit { get; private set; }
        public DoorUnit DoorUnit { get; private set; }
        public SeashellGripper SeashellGripper { get; private set; }
        public FishingRod FishingRod { get; private set; }
        public SandGripper SandGripper { get; private set; }
        public ParasolUnit ParasolUnit { get; private set; }

        private HashSet<string> grippedFish = new HashSet<string>();

        public override IEnumerable<IUnit> Units
        {
            get
            {
                yield return DoorUnit;
                yield return FishingRod;
                yield return ParasolUnit;
                yield return SandGripper;
                yield return SeashellGripper;
                yield return SimpleMovementUnit;
            }
        }

        public override void AdditionalInitialization()
        {
            base.AdditionalInitialization();

            SimpleMovementUnit = new SimpleMovementUnit(this);
            DoorUnit = new DoorUnit(this, World, new Frame3D(15, 0, 5));
            FishingRod = new FishingRod(this, World, new Frame3D(20, 0, 10));
            SandGripper = new SandGripper(this, World, new Frame3D(-15, 0, 5));
            SeashellGripper = new SeashellGripper(this, World, new Frame3D(15, 0, 5));
            ParasolUnit = new ParasolUnit(this, World);

            FishingRod.OnGrip = DoorUnit.OnActivation = DoorUnit.OnDeactivation = id =>
            {
                if (!IsValidObject(id)) World.Scores.Add(ControllerId, -20, "Penalty");
            };

            SeashellGripper.OnGrip = id =>
            {
                var seashellColor = World.IdGenerator.GetKey<TBBObject>(id).Color;
                if (seashellColor != RobotColor && seashellColor != SideColor.Any)
                    World.Scores.Add(ControllerId, -20, "Penalty");
            };

            ParasolUnit.OnActivation = _ => World.Scores.Add(ControllerId, 20, "Open parasol");
            ParasolUnit.OnDeactivation = _ => World.Scores.Add(ControllerId, -20, "Close parasol");

            DoorUnit.OnActivation += id =>
            {
                if (IsValidObject(id)) World.Scores.Add(ControllerId, 10, "Raise valid flag");
            };

            DoorUnit.OnDeactivation += id =>
            {
                if (IsValidObject(id)) World.Scores.Add(ControllerId, -10, "Release valid flag");
            };
        }

        private bool IsValidObject(string id)
        {
            return World.IdGenerator.GetKey<TBBObject>(id).Color == RobotColor;
        }

        private SideColor RobotColor
        {
            get { return ControllerId == TwoPlayersId.Left ? SideColor.Violet : SideColor.Green; }
        }
    }
}
