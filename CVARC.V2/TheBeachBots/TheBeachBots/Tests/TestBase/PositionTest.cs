using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots
{
    class PositionTest : TBBTestBase
    {
        public PositionTest(double x, double y, double angle, double delta, params TBBCommand[] commands)
            : base((data, asserter) =>
            {
                asserter.IsEqual(x, data.SelfLocation.X, delta);
                asserter.IsEqual(y, data.SelfLocation.Y, delta);
                asserter.IsEqual(angle, data.SelfLocation.Angle, delta);
            }, commands) { }

        public PositionTest(double x, double y, double delta, params TBBCommand[] commands)
            : base((data, asserter) =>
            {
                asserter.IsEqual(x, data.SelfLocation.X, delta);
                asserter.IsEqual(y, data.SelfLocation.Y, delta);
            }, commands) { }
    }
}
