using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AIRLab;

namespace CVARC.V2
{
    public interface ICvarcUnitTest
    {
        IEnumerable<Tuple<string, ICvarcTest>> GetDefinedTests();
    }

    public abstract class CvarcUnitTest<TRules, TSensorData, TCommand, TWorldState, TWorld> : ICvarcUnitTest
        where TSensorData : class, new()
        where TWorldState : class, IWorldState
        where TCommand : class, ISimpleMovementCommand 
        where TWorld : IWorld
        where TRules : IRules
    {
        protected CommandBuilder<TRules, TCommand> Robot { get; }

        private readonly ReflectableTestBuilder<TSensorData, TCommand, TWorldState, TWorld> _testBuilder;
        protected SettingsProposal DefaultSettings { get; }
        protected TWorldState DefaultWorldState { get; }

        protected CvarcUnitTest(TRules rules, TWorldState worldState, SettingsProposal defaultSettings)
        {
            _testBuilder = new ReflectableTestBuilder<TSensorData, TCommand, TWorldState, TWorld>();
            DefaultSettings = defaultSettings;
            DefaultWorldState = worldState;

            Robot = new CommandBuilder<TRules, TCommand>(rules);
            Robot.CommandAdded += c => _testBuilder.AddCommand(c);
        }

        public IEnumerable<Tuple<string, ICvarcTest>> GetDefinedTests()
        {
            return GetType()
                .GetMethods()
                .Where(m => m.GetCustomAttributes(true).Count(a => a is CvarcTestMethod) != 0)
                .Select(BuildTest);
        }

        private Tuple<string, ICvarcTest> BuildTest(MethodInfo method)
        {
            method.Invoke(this, new object[0]);
            var settings = MutateSettings(method, DefaultSettings);
            var worldState = MutateWorldState(method, DefaultWorldState);
            var reflected = IsReflected(method);
            return Tuple.Create(method.Name, (ICvarcTest)_testBuilder.CreateTest(worldState, settings, reflected));
        }

        private static bool IsReflected(MethodInfo method)
        {
            return method
                .GetCustomAttributes(true)
                .Where(a => a is TestSettings.SetReflected)
                .Any(a => (a as TestSettings.SetReflected)?.Reflected ?? false);
        }

        private static TWorldState MutateWorldState(MethodInfo method, TWorldState defaultWorldState)
        {
            return method
                .GetCustomAttributes(true)
                .Where(a => a is TestSettings.WorldState)
                .Select(a => ((a as TestSettings.WorldState)?.State as TWorldState))
                .FirstOrDefault() ?? defaultWorldState;
        }

        private static SettingsProposal MutateSettings(MethodInfo method, SettingsProposal source)
        {
            var newSettings = SettingsProposal.DeepCopy(source);
            return method.GetCustomAttributes(true)
                .Where(attr => attr is SettingsProposalMutator)
                .Aggregate(newSettings, (set, mut) => (mut as SettingsProposalMutator)?.Mutate(set));
        }

        protected void AssertEqual(Func<TSensorData, double> actual, double expected, double delta)
        {
            _testBuilder.AddAssert((s, a) => a.IsEqual(expected, actual(s), delta));
        }

        protected void AssertTrue(Func<TSensorData, bool> condition)
        {
            AssertBool(condition, true);
        }

        protected void AssertFalse(Func<TSensorData, bool> condition)
        {
            AssertBool(condition, false);
        }

        private void AssertBool(Func<TSensorData, bool> condition, bool expected)
        {
            _testBuilder.AddAssert((s, a) => a.IsEqual(expected, condition(s)));
        }
    }
}
