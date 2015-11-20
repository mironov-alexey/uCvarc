using System;
using System.Linq;
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

            //LoadDoorTest(logicPart, rules);
            //LoadFishingTest(logicPart, rules);
            //LoadTestExample(logicPart, rules);
            //LoadScoresTest(logicPart, rules);

            LoadTests(logicPart);

            return logicPart;
        }

        void AddTest(LogicPart logic, string name, ICvarcTest test)
        {
            logic.Tests[name] = test;
        }
        
        private void LoadTests(LogicPart logic)
        {
            GetType().Assembly.GetTypes()
                .Where(type => type.GetCustomAttributes(true).Count(a => a is CvarcTestClass) != 0)
                .Select(type => Activator.CreateInstance(type) as ICvarcUnitTest)
                .SelectMany(instance => instance?.GetDefinedTests()).ToList()
                .ForEach(test => AddTest(logic, test.Item1, test.Item2));
        }
    }
}
