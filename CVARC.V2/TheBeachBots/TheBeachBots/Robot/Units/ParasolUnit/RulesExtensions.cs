using System.Windows.Forms;

namespace CVARC.V2
{
    public static partial class RulesExtensions
	{

		public static void AddParasolUnitKeys<TCommand>(this IParasolRules<TCommand> rules, 
            KeyboardController<TCommand> pool, string controllerId)
			where TCommand : IParasolCommand, new()
		{
			if (controllerId == TwoPlayersId.Left)
			{
                pool.Add(Keys.NumPad8, () => new TCommand { ParasolAction = TriggerAction.Activate });
			}
			if (controllerId == TwoPlayersId.Right)
			{
                pool.Add(Keys.NumPad1, () => new TCommand { ParasolAction = TriggerAction.Activate });
			}
		}

        public static TCommand DeactivateParasol<TCommand>(this IParasolRules<TCommand> factory)
           where TCommand : IParasolCommand, new()
        {
            return new TCommand { ParasolAction = TriggerAction.Deactivate };
        }

        public static CommandBuilder<TRules, TCommand, TBack> DeactivateParasol<TRules, TCommand, TBack>
            (this CommandBuilder<TRules, TCommand, TBack> builder)
            where TCommand : IParasolCommand, new()
            where TRules : IParasolRules<TCommand>
        {
            builder.Add(DeactivateParasol(builder.Rules));
            return builder;
        }

        public static TCommand ActivateParasol<TCommand>(this IParasolRules<TCommand> factory)
            where TCommand : IParasolCommand, new()
        {
            return new TCommand { ParasolAction = TriggerAction.Activate };
        }

        public static CommandBuilder<TRules, TCommand, TBack> ActivateParasol<TRules, TCommand, TBack>
            (this CommandBuilder<TRules, TCommand, TBack> builder)
            where TCommand : IParasolCommand, new()
            where TRules : IParasolRules<TCommand>
        {
            builder.Add(ActivateParasol(builder.Rules));
            return builder;
        }
    }
}
