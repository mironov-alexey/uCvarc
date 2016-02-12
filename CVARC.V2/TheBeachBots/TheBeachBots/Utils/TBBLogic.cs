using System;
using System.Linq;
using CVARC.V2;

namespace TheBeachBots
{
    public partial class TBBLogicPartHelper : TestLoadableLogicPartHelper
    {
        public override LogicPart Initialize(LogicPart logicPart)
        {
            var rules = TBBRules.Current;
            
            logicPart.CreateWorld = () => new TBBWorld();
            logicPart.CreateDefaultSettings = () => new Settings { OperationalTimeLimit = 5, TimeLimit = 90 };
            logicPart.CreateWorldState = name => new TBBWorldState(UInt16.Parse(name));
            logicPart.PredefinedWorldStates.AddRange(Enumerable.Range(0, 5).Select(z => z.ToString()));
            logicPart.WorldStateType = typeof(TBBWorldState);

            var actorFactory = ActorFactory.FromRobot(new TBBRobot(), rules);
            logicPart.Actors[TwoPlayersId.Left] = actorFactory;
            logicPart.Actors[TwoPlayersId.Right] = actorFactory;

            logicPart.Bots["Standing"] = () => rules.CreateStandingBot();

            return logicPart;
        }
    }
}
