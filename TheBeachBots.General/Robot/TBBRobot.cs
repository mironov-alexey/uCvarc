using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots
{
    class TBBRobot : Robot<ITBBActorManager, TBBWorld, TBBSensorsData, TBBCommand, TBBRules>
    {
        public SimpleMovementUnit SimpleMovementUnit { get; private set; }

        public override IEnumerable<IUnit> Units
        {
            get
            {
                yield return SimpleMovementUnit;
            }
        }

        public override void AdditionalInitialization()
        {
            base.AdditionalInitialization();

            SimpleMovementUnit = new SimpleMovementUnit(this);
        }
    }
}
