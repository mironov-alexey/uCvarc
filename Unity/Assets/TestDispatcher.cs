using System;
using System.Collections.Generic;
using System.Threading;
using CVARC.V2;

namespace Assets
{
    public static class TestDispatcher
    {
        public static readonly Dictionary<string, bool> LastTestExecution = new Dictionary<string, bool>();
        static readonly HashSet<Thread> runnedTests = new HashSet<Thread>();

        public static void RunOneTest(LoadingData data, string testName)
        {
            Debugger.Log(DebuggerMessageType.Unity, "Starting test " + testName);
            var test = Dispatcher.Loader.GetTest(data, testName);
            var asserter = new UnityAsserter(testName);

            var testThread = new Thread(() => ExecuteTest(testName, test, asserter, MakeServerInfo(data)));
            runnedTests.Add(testThread);
            testThread.Start();
        }

        public static void RunAllTests(LoadingData data)
        {
            var competitions = Dispatcher.Loader.GetCompetitions(data);
            var testsNames = competitions.Logic.Tests.Keys;

            var testThread = new Thread(() => ExecuteTests(testsNames, data));
            runnedTests.Add(testThread);
            testThread.Start();
        }

        public static void OnDispose()
        {
            foreach (var thread in runnedTests)
                if (thread.IsAlive)
                    thread.Abort(); // зависания. Мне кажется проблема в том, что test.Run создает еще треды...
        }

        static void ExecuteTests(IEnumerable<string> testNames, LoadingData data)
        {
            Debugger.Log(DebuggerMessageType.Unity, "staring tests");
            foreach (var testName in testNames)
            {
                var asserter = new UnityAsserter(testName);
                Debugger.Log(DebuggerMessageType.Unity, "Test is ready");
                var test = Dispatcher.Loader.GetTest(data, testName);
                ExecuteTest(testName, test, asserter, MakeServerInfo(data));
            }
        }

        static void ExecuteTest(string testName, ICvarcTest test, UnityAsserter asserter, NetworkServerData networkServerInfo)
        {
            try
            {
                test.Run(networkServerInfo, asserter);
                asserter.DebugOkMessage();
            }
            catch (Exception e)
            {
                asserter.Fail(e.GetType().Name + " " + e.Message);
            }
            lock (LastTestExecution)
            {
                LastTestExecution[testName] = !asserter.Failed;
            }
        }

        static void WaitWorld(NetworkServerData data)
        {
            var myName = data.LoadingData.AssemblyName + data.LoadingData.Level;
            while (Dispatcher.currentRunner == null ||
                   Dispatcher.currentRunner.Name != myName ||
                   Dispatcher.currentRunner.World == null)
                Thread.Sleep(5);
            data.World = Dispatcher.currentRunner.World;

        }

        static NetworkServerData MakeServerInfo(LoadingData data)
        {
            var networkServerInfo = new NetworkServerData { Port = 14000, LoadingData = data, ServerLoaded = true };
            networkServerInfo.WaitWorld += WaitWorld;
            return networkServerInfo;
        }
    }
}
