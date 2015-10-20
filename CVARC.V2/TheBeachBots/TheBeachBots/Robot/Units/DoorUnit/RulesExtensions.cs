using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CVARC.V2
{
	public static partial class RulesExtensions
	{

		public static void AddDoorUnitKeys<TCommand>(this IDoorOpeningRules<TCommand> rules, 
            KeyboardController<TCommand> pool, string controllerId)
			where TCommand : IDoorUnitCommand, new()
		{
			if (controllerId == TwoPlayersId.Left)
			{
                pool.Add(Keys.Q, () => new TCommand { DoorUnitAction = TriggerAction.Deactivate });
			}
			if (controllerId == TwoPlayersId.Right)
			{
                pool.Add(Keys.U, () => new TCommand { DoorUnitAction = TriggerAction.Activate });
			}
		}

        public static TCommand CloseDoor<TCommand>(this IDoorOpeningRules<TCommand> factory)
           where TCommand : IDoorUnitCommand, new()
        {
            return new TCommand { DoorUnitAction = TriggerAction.Activate };
        }

        public static TCommand OpenDoor<TCommand>(this IDoorOpeningRules<TCommand> factory)
            where TCommand : IDoorUnitCommand, new()
        {
            return new TCommand { DoorUnitAction = TriggerAction.Deactivate };
        }
	}
}
