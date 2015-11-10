using System.Windows.Forms;

namespace CVARC.V2
{
    public static partial class RulesExtensions
	{
		public static void AddFishingKeys<TCommand>(this IFishingRules<TCommand> rules, 
            KeyboardController<TCommand> pool, string controllerId)
			where TCommand : IFishingCommand, new()
		{
			if (controllerId == TwoPlayersId.Left)
			{
                pool.Add(Keys.Z, () => new TCommand { FishingRodAction = FishingRodAction.GripFish });
                pool.Add(Keys.X, () => new TCommand { FishingRodAction = FishingRodAction.ReleaseFish });
			}
			if (controllerId == TwoPlayersId.Right)
			{
                pool.Add(Keys.N, () => new TCommand { FishingRodAction = FishingRodAction.GripFish });
                pool.Add(Keys.M, () => new TCommand { FishingRodAction = FishingRodAction.ReleaseFish });
			}
		}

        public static TCommand GripFish<TCommand>(this IFishingRules<TCommand> factory)
           where TCommand : IFishingCommand, new()
        {
            return new TCommand { FishingRodAction = FishingRodAction.GripFish };
        }

        public static CommandBuilder<TRules, TCommand, TBack> GripFish<TRules, TCommand, TBack>
            (this CommandBuilder<TRules, TCommand, TBack> builder)
            where TCommand : IFishingCommand, new()
            where TRules : IFishingRules<TCommand>
        {
            builder.Add(GripFish(builder.Rules));
            return builder;
        }

        public static TCommand ReleaseFish<TCommand>(this IFishingRules<TCommand> factory)
            where TCommand : IFishingCommand, new()
        {
            return new TCommand { FishingRodAction = FishingRodAction.ReleaseFish };
        }

        public static CommandBuilder<TRules, TCommand, TBack> ReleaseFish<TRules, TCommand, TBack>
            (this CommandBuilder<TRules, TCommand, TBack> builder)
            where TCommand : IFishingCommand, new()
            where TRules : IFishingRules<TCommand>
        {
            builder.Add(ReleaseFish(builder.Rules));
            return builder;
        }
    }
}
