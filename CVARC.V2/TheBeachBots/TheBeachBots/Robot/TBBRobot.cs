using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override IEnumerable<IUnit> Units
        {
            get
            {
                yield return DoorUnit;
                yield return FishingRod;
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
            FishingRod = new FishingRod(this, World, new Frame3D(20, 0, 10), 15);
            SandGripper = new SandGripper(this, World, new Frame3D(-15, 0, 5), 5);
            SeashellGripper = new SeashellGripper(this, World, new Frame3D(15, 0, 5));
        }
    }
}
