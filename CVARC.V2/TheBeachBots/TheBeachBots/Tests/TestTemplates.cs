using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;

namespace TheBeachBots
{
    public partial class TBBLogicPartHelper
    {
        [AttributeUsage(AttributeTargets.Method)]
        class TestLoaderMethod : Attribute { }

        //TBBTestEntry TowerBuilderTest(int count, params TBBCommand[] commands)
        //{
        //    return TestTemplate((data, asserter) => asserter.IsEqual(count, data.CollectedDetailsCount, 0), commands);
        //}

        //TBBTestEntry PopCornTest(int count, params TBBCommand[] commands)
        //{
        //    return TestTemplate((data, asserter) => asserter.IsEqual(count, data.LoadedPopCornCount, 0), commands);
        //}

        //TBBTestEntry ScoreTest(int scoreCount, params TBBCommand[] commands)
        //{
        //    return TestTemplate((data, asserter) => asserter.IsEqual(scoreCount, data.MyScores, 0), commands);
        //}

        //TBBTestEntry PositionTest(double x, double y, double angle, double delta, params TBBCommand[] commands)
        //{
        //    return TestTemplate((data, asserter) => { 
        //            asserter.IsEqual(x, data.SelfLocation.X, delta);
        //            asserter.IsEqual(y, data.SelfLocation.Y, delta);
        //            asserter.IsEqual(angle, data.SelfLocation.Angle, delta);
        //        }, commands);
        //}

        //TBBTestEntry PositionTest(double x, double y, double delta, params TBBCommand[] commands)
        //{
        //    return TestTemplate((data, asserter) => { 
        //            asserter.IsEqual(x, data.SelfLocation.X, delta);
        //            asserter.IsEqual(y, data.SelfLocation.Y, delta);
        //        }, commands);
        //}
        
        TBBTestEntry TestTemplate(Action<TBBSensorsData, IAsserter> assert, params TBBCommand[] commands)
        {
            return (client, world, asserter) =>
            {
                var data = new TBBSensorsData();
                foreach (var c in commands)
                    data = client.Act(c);
                assert(data, asserter);
            };
        }

        void AddTest(LogicPart logic, string name, TBBTestEntry test)
        {
            logic.Tests[name] = new RMTestBase(test, new TBBWorldState(0xdead));
        }
    }
}
