using CVARC.V2;
using AIRLab.Mathematics;
using System.Collections.Generic;

namespace TheBeachBots
{
    class TBBTestBuilder : ReflectableTestBuilder<TBBSensorsData, TBBCommand, TBBWorldState, TBBWorld>
    {
        TBBRules rules;

        public TBBTestBuilder(TBBRules rules, TBBWorldState worldState, SettingsProposal settings)
            : base(worldState, settings)
        {
            this.rules = rules;
        }

        public TBBTestBuilder(TBBRules rules, TBBWorldState worldState)
            : this(rules, worldState, new SettingsProposal() { Controllers = new List<ControllerSettings>() })
        {
            Settings.OperationalTimeLimit = 5;
            Settings.TimeLimit = 90;
            Settings.SpeedUp = false;
            AddControllerSettings(TwoPlayersId.Left, "This", ControllerType.Client);
            AddControllerSettings(TwoPlayersId.Right, "Standing", ControllerType.Bot);
        }

        public TBBTestBuilder Assert(Asserter<TBBSensorsData, TBBWorld> assert)
        {
            AddTestAction(assert);
            return this;
        }

        public TBBTestBuilder AssertHasFish(bool expected)
        {
            AddTestAction((s, w, a) => a.IsEqual(expected, s.FishAttached));
            return this;
        }

        public TBBTestBuilder AssertHasSeashell(bool expected)
        {
            AddTestAction((s, w, a) => a.IsEqual(expected, s.SeashellAttached));
            return this;
        }

        public TBBTestBuilder AssertCollectedSandCount(int expected)
        {
            AddTestAction((s, w, a) => a.IsEqual(expected, s.CollectedSandCount, 0));
            return this;
        }

        public TBBTestBuilder AssertLocation(double x, double y, double angle, double delta)
        {
            AddTestAction((s, w, a) => 
            {
                a.IsEqual(x, s.SelfLocation.X, delta);
                a.IsEqual(y, s.SelfLocation.Y, delta);
                a.IsEqual(angle, s.SelfLocation.Angle, delta);
            });

            return this;
        }

        public TBBTestBuilder AssertLocation(double x, double y, double delta)
        {
            AddTestAction((s, w, a) =>
            {
                a.IsEqual(x, s.SelfLocation.X, delta);
                a.IsEqual(y, s.SelfLocation.Y, delta);
            });

            return this;
        }

        public TBBTestBuilder AssertScores(int scores)
        {
            AddTestAction((s, w, a) => a.IsEqual(scores, s.SelfScores, 0));
            return this;
        }

        public TBBTestBuilder Move(double length)
        {
            AddTestAction(rules.Move(length));
            return this;
        }

        public TBBTestBuilder Rotate(Angle angle)
        {
            AddTestAction(rules.Rotate(angle));
            return this;
        }

        public TBBTestBuilder Stand(double time)
        {
            AddTestAction(rules.Stand(time));
            return this;
        }

        public TBBTestBuilder OpenDoor()
        {
            AddTestAction(rules.OpenDoor());
            return this;
        }

        public TBBTestBuilder CloseDoor()
        {
            AddTestAction(rules.CloseDoor());
            return this;
        }

        public TBBTestBuilder GripFish()
        {
            AddTestAction(rules.GripFish());
            return this;
        }

        public TBBTestBuilder ReleaseFish()
        {
            AddTestAction(rules.ReleaseFish());
            return this;
        }

        public TBBTestBuilder GripSeashell()
        {
            AddTestAction(rules.GripSeashell());
            return this;
        }

        public TBBTestBuilder ReleaseSeashell()
        {
            AddTestAction(rules.ReleaseSeashell());
            return this;
        }

        public TBBTestBuilder CollectSandBlock()
        {
            AddTestAction(rules.CollectSandBlock());
            return this;
        }

        public TBBTestBuilder ReleaseSandBlock()
        {
            AddTestAction(rules.ReleaseSandBlock());
            return this;
        }
    }
}
