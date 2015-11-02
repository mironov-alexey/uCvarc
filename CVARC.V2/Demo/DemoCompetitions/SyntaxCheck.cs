﻿using CVARC.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{

    public class CameraTestBuilder : TestBuilder<DemoRules,DemoSensorsData, DemoCommand, DemoWorldState, DemoWorld>
    {
        public CameraTestBuilder() : base(new DemoRules(), KnownWorldStates.EmptyWithOneRobot(true), new SettingsProposal { TimeLimit=10 })
        {
            AddControllerSettings(TwoPlayersId.Left, "This", ControllerType.Client);
            AddControllerSettings(TwoPlayersId.Right, "Stand", ControllerType.Bot);
        }

        public CameraTestBuilder Assert(Asserter<DemoSensorsData> assert)
        {
            base.AddTestAction(assert);
            return this;
        }
    }

    class SyntaxCheck
    {
        public static void Check()
        {
            var b = new CameraTestBuilder();
            b.Builder.Grip().Release().To(() => b).Assert((a,v) => { }).CreateTest();

        }
    }
}
