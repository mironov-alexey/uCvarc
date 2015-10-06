﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots
{
    public partial class TBBLogicPartHelper : LogicPartHelper 
    {
        public override LogicPart Create()
        {
            var rules = TBBRules.Current;

            var logicPart = new LogicPart();
            logicPart.CreateWorld = () => new TBBWorld();
            logicPart.CreateDefaultSettings = () => new Settings { OperationalTimeLimit = 5, TimeLimit = 90 };
            logicPart.CreateWorldState = name => new TBBWorldState(UInt16.Parse(name));
            logicPart.PredefinedWorldStates.AddRange(Enumerable.Range(0, 5).Select(z => z.ToString()));
            logicPart.WorldStateType = typeof(TBBWorldState);

            var actorFactory = ActorFactory.FromRobot(new TBBRobot(), rules);
            logicPart.Actors[TwoPlayersId.Left] = actorFactory;
            logicPart.Actors[TwoPlayersId.Right] = actorFactory;

            logicPart.Bots["Standing"] = () => rules.CreateStandingBot();

            LoadTests(logicPart, rules);

            return logicPart;
        }

        private void LoadTests(LogicPart logic, TBBRules rules)
        {
            var testMethods = GetType()
                .GetMethods()
                .Where(m => m.GetCustomAttributes(true).Where(a => a is TestLoaderMethod).Count() != 0)
                .Select(m => m.Name);

            foreach (var name in testMethods)
                GetType().GetMethod(name).Invoke(this, new Object[] { logic, rules });
        }
    }
}
