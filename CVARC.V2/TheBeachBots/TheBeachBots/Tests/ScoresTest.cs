using CVARC.V2;

namespace TheBeachBots
{
    partial class TBBLogicPartHelper
    {
        void LoadScoresTest(LogicPart logic, TBBRules rules)
        {
            var builder = new TBBTestBuilder(rules, new TBBWorldState(0));

            logic.Tests["Scores_Zero-IfNothingHappened"] = builder
                .Commands
                    .Stand(5)
                .Back
                    .AssertScores(0)                
                    .CreateTest();
        }
    }
}
