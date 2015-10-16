using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    class TBBTestBuilder : ReflectableTestBuilder<TBBSensorsData, TBBCommand, TBBWorldState, TBBWorld, TBBRules>
    {
        public TBBTestBuilder(LogicPart logic, TBBRules rules, TBBWorldState worldState)
            : base(logic, rules, worldState) { }

        public TBBTestBuilder CreateTest(string name)
        {
            CreateClearData(name);
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
            AddAction(Rules.Move(length));
            return this;
        }

        public TBBTestBuilder Rotate(Angle angle)
        {
            AddAction(Rules.Rotate(angle));
            return this;
        }

        public TBBTestBuilder Stand(double time)
        {
            AddAction(Rules.Stand(time));
            return this;
        }

        public TBBTestBuilder OpenDoor()
        {
            AddAction(Rules.OpenDoor());
            return this;
        }

        public TBBTestBuilder CloseDoor()
        {
            AddAction(Rules.CloseDoor());
            return this;
        }

        public TBBTestBuilder GripFish()
        {
            AddAction(Rules.GripFish());
            return this;
        }

        public TBBTestBuilder ReleaseFish()
        {
            AddAction(Rules.ReleaseFish());
            return this;
        }

        public TBBTestBuilder GripSeashell()
        {
            AddAction(Rules.GripSeashell());
            return this;
        }

        public TBBTestBuilder ReleaseSeashell()
        {
            AddAction(Rules.ReleaseSeashell());
            return this;
        }
    }
}
