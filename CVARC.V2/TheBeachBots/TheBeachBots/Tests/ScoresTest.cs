using CVARC.V2;

namespace TheBeachBots
{
    partial class TBBLogicPartHelper
    {
        void LoadScoresTest(LogicPart logic, TBBRules rules)
        {
            var builder = new TBBTestBuilder(rules, new TBBWorldState(0));

            logic.Tests["Scores_Zero-IfNothingHappened"] = builder
                .Stand(5)
                .AssertScores(0)                
                .CreateTest();
        }
    }
}
