using System;
using CVARC.V2;
using System.Collections.Generic;

namespace TheBeachBots
{
    //class TBBTestBuilder : ReflectableTestBuilder<TBBRules, TBBSensorsData, TBBCommand, TBBWorldState, TBBWorld>
    //{
    //    public readonly CommandBuilder<TBBRules, TBBCommand, TBBTestBuilder> Commands; 

    //    public TBBTestBuilder(TBBRules rules, TBBWorldState worldState)
    //        : base(rules, worldState, new SettingsProposal())
    //    {
    //        Settings.OperationalTimeLimit = 5;
    //        Settings.TimeLimit = 90;
    //        Settings.SpeedUp = false;
    //        Settings.Controllers = new List<ControllerSettings>();
    //        AddControllerSettings(TwoPlayersId.Left, "This", ControllerType.Client);
    //        AddControllerSettings(TwoPlayersId.Right, "Standing", ControllerType.Bot);
    //        Commands = new CommandBuilder<TBBRules, TBBCommand, TBBTestBuilder>(rules, this);
    //        Commands.CommandAdded += AddCommand;
    //    }

    //    public TBBTestBuilder Assert(Action<TBBSensorsData, IAsserter> assert)
    //    {
    //        AddAssert(assert);
    //        return this;
    //    }

    //    public TBBTestBuilder AssertHasFish(bool expected)
    //    {
    //        AddAssert((s,  a) => a.IsEqual(expected, s.FishAttached));
    //        return this;
    //    }

    //    public TBBTestBuilder AssertHasSeashell(bool expected)
    //    {
    //        AddAssert((s,  a) => a.IsEqual(expected, s.SeashellAttached));
    //        return this;
    //    }

    //    public TBBTestBuilder AssertCollectedSandCount(int expected)
    //    {
    //        AddAssert((s,  a) => a.IsEqual(expected, s.CollectedSandCount, 0));
    //        return this;
    //    }

    //    public TBBTestBuilder AssertScores(int scores)
    //    {
    //        AddAssert((s,  a) => a.IsEqual(scores, s.SelfScores, 0));
    //        return this;
    //    }
    //}
}
