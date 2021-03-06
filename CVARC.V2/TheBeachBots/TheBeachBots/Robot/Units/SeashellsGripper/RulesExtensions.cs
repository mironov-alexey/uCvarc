﻿using System.Windows.Forms;

namespace CVARC.V2
{
    public static partial class RulesExtensions
	{
		public static void AddSeashellGripperKeys<TCommand>(this ISeashellGripperRules<TCommand> rules, 
            KeyboardController<TCommand> pool, string controllerId)
			where TCommand : ISeashellGripperCommand, new()
		{
			if (controllerId == TwoPlayersId.Left)
			{
                pool.Add(Keys.R, () => new TCommand { SeashellGripperAction = SeashellGripperAction.Grip });
                pool.Add(Keys.F, () => new TCommand { SeashellGripperAction = SeashellGripperAction.Release });
			}
			if (controllerId == TwoPlayersId.Right)
			{
                pool.Add(Keys.P, () => new TCommand { SeashellGripperAction = SeashellGripperAction.Grip });
                pool.Add(Keys.OemSemicolon, () => new TCommand { SeashellGripperAction = SeashellGripperAction.Release });
			}
		}

        public static TCommand GripSeashell<TCommand>(this ISeashellGripperRules<TCommand> factory)
           where TCommand : ISeashellGripperCommand, new()
        {
            return new TCommand { SeashellGripperAction = SeashellGripperAction.Grip };
        }

        public static CommandBuilder<TRules, TCommand> GripSeashell<TRules, TCommand>
            (this CommandBuilder<TRules, TCommand> builder)
            where TCommand : ISeashellGripperCommand, new()
            where TRules : ISeashellGripperRules<TCommand>
        {
            builder.Add(GripSeashell(builder.Rules));
            return builder;
        }

        public static TCommand ReleaseSeashell<TCommand>(this ISeashellGripperRules<TCommand> factory)
            where TCommand : ISeashellGripperCommand, new()
        {
            return new TCommand { SeashellGripperAction = SeashellGripperAction.Release };
        }

        public static CommandBuilder<TRules, TCommand> ReleaseSeashell<TRules, TCommand>
            (this CommandBuilder<TRules, TCommand> builder)
            where TCommand : ISeashellGripperCommand, new()
            where TRules : ISeashellGripperRules<TCommand>
        {
            builder.Add(ReleaseSeashell(builder.Rules));
            return builder;
        }
    }
}
