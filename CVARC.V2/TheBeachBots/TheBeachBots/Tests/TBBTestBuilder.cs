using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    class TBBTestBuilder : ReflectableTestBuilder<TBBSensorsData, TBBCommand, TBBWorldState, TBBWorld>
    {
        TBBRules rules;

        public TBBTestBuilder(LogicPart logic, TBBRules rules, TBBWorldState worldState)
            : base(logic, worldState)
        {
            this.rules = rules;
        }

        public TBBTestBuilder CreateTest(string name)
        {
            CreateClearData(name);
            return this;
        }

        public TBBTestBuilder AssertHasFish(bool expected)
        {
            AddAction((s, w, a) => a.IsEqual(expected, s.FishAttached));
            return this;
        }

        public TBBTestBuilder AssertHasSeashell(bool expected)
        {
            AddAction((s, w, a) => a.IsEqual(expected, s.SeashellAttached));
            return this;
        }

        public TBBTestBuilder AssertCollectedSandCount(int expected)
        {
            AddAction((s, w, a) => a.IsEqual(expected, s.CollectedSandCount, 0));
            return this;
        }

        public TBBTestBuilder AssertLocation(double x, double y, double angle, double delta)
        {
            AddAction((s, w, a) => 
            {
                a.IsEqual(x, s.SelfLocation.X, delta);
                a.IsEqual(y, s.SelfLocation.Y, delta);
                a.IsEqual(angle, s.SelfLocation.Angle, delta);
            });

            return this;
        }

        public TBBTestBuilder AssertLocation(double x, double y, double delta)
        {
            AddAction((s, w, a) =>
            {
                a.IsEqual(x, s.SelfLocation.X, delta);
                a.IsEqual(y, s.SelfLocation.Y, delta);
            });

            return this;
        }

        public TBBTestBuilder AssertScores(int scores)
        {
            AddAction((s, w, a) => a.IsEqual(scores, s.SelfScores, 0));
            return this;
        }

        public TBBTestBuilder Move(double length)
        {
            AddAction(rules.Move(length));
            return this;
        }

        public TBBTestBuilder Rotate(Angle angle)
        {
            AddAction(rules.Rotate(angle));
            return this;
        }

        public TBBTestBuilder Stand(double time)
        {
            AddAction(rules.Stand(time));
            return this;
        }

        public TBBTestBuilder OpenDoor()
        {
            AddAction(rules.OpenDoor());
            return this;
        }

        public TBBTestBuilder CloseDoor()
        {
            AddAction(rules.CloseDoor());
            return this;
        }

        public TBBTestBuilder GripFish()
        {
            AddAction(rules.GripFish());
            return this;
        }

        public TBBTestBuilder ReleaseFish()
        {
            AddAction(rules.ReleaseFish());
            return this;
        }

        public TBBTestBuilder GripSeashell()
        {
            AddAction(rules.GripSeashell());
            return this;
        }

        public TBBTestBuilder ReleaseSeashell()
        {
            AddAction(rules.ReleaseSeashell());
            return this;
        }

        public TBBTestBuilder CollectSandBlock()
        {
            AddAction(rules.CollectSandBlock());
            return this;
        }

        public TBBTestBuilder ReleaseSandBlock()
        {
            AddAction(rules.ReleaseSandBlock());
            return this;
        }
    }
}
