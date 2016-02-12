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

        public static CommandBuilder<TRules, TCommand> CloseDoor<TRules, TCommand>
            (this CommandBuilder<TRules, TCommand> builder)
            where TCommand : IDoorUnitCommand, new()
            where TRules : IDoorOpeningRules<TCommand>
        {
            builder.Add(CloseDoor(builder.Rules));
            return builder;
        }

        public static TCommand OpenDoor<TCommand>(this IDoorOpeningRules<TCommand> factory)
            where TCommand : IDoorUnitCommand, new()
        {
            return new TCommand { DoorUnitAction = TriggerAction.Deactivate };
        }

        public static CommandBuilder<TRules, TCommand> OpenDoor<TRules, TCommand>
            (this CommandBuilder<TRules, TCommand> builder)
            where TCommand : IDoorUnitCommand, new()
            where TRules : IDoorOpeningRules<TCommand>
        {
            builder.Add(OpenDoor(builder.Rules));
            return builder;
        }
    }
}
