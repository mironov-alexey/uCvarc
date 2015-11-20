using CVARC.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{

    //public class CameraTestBuilder : TestBuilder<DemoRules,DemoSensorsData, DemoCommand, DemoWorldState, DemoWorld>
    //{
    //    public readonly CommandBuilder<DemoRules, DemoCommand, CameraTestBuilder> Commands;

    //    public CameraTestBuilder() : base(new DemoRules(), KnownWorldStates.EmptyWithOneRobot(true), new SettingsProposal { TimeLimit=10 })
    //    {
    //        AddControllerSettings(TwoPlayersId.Left, "This", ControllerType.Client);
    //        AddControllerSettings(TwoPlayersId.Right, "Stand", ControllerType.Bot);
    //        Commands = new CommandBuilder<DemoRules, DemoCommand, CameraTestBuilder>(Rules, this);
    //        Commands.CommandAdded += AddCommand;
    //    }

    //    public CameraTestBuilder Assert(Action<DemoSensorsData, IAsserter> assert)
    //    {
    //        AddAssert(assert);
    //        return this;
    //    }
    //}

    //class SyntaxCheck
    //{
    //    public static void Check()
    //    {
    //        var b = new CameraTestBuilder();
    //        b
    //            .Commands
    //                .Grip()
    //                .Release()
    //            .Back
    //                .Assert((a,v) => { })
    //                .CreateTest();
    //    }
    //}
}
