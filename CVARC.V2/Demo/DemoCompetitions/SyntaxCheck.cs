using CVARC.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Demo
{
    class SyntaxCheck
    {
        public static void Check()
        {
            var rules = new DemoRules();
            var builder = new CommandBuilder<DemoRules, DemoCommand>(rules);
            builder.Grip().Release();
        }
    }
}
