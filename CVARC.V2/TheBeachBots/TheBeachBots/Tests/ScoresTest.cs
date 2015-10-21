using CVARC.V2;

namespace TheBeachBots
{
    partial class TBBLogicPartHelper
    {
        [TestLoaderMethod]
        void LoadScoresTest(LogicPart logic, TBBRules rules)
        {
            var builder = new TBBTestBuilder(logic, rules, new TBBWorldState(0));

            builder.CreateTest("Scores_Zero-IfNothingHappened")
                .Stand(5)
                .AssertScores(0)                
                .EndOfTest();
        }
    }
}
