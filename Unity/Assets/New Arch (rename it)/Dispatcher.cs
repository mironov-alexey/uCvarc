using System.Collections.Generic;
using CVARC.V2;
using RoboMovies;
using UnityEngine;

namespace Assets.Temp
{
    public static class Dispatcher
    {
        public static Loader Loader { get; private set; }
        public static readonly Queue<IRunner> Queue = new Queue<IRunner>(); // не уверен, не зло ли я посадил?
        static PercistentTCPServer server;
        static IRunner currentRunner;

        public static void Start()
        {
            Debugger.DisableByDefault = true;
            Debugger.EnabledTypes.Add(DebuggerMessageType.Unity);
            Debugger.EnabledTypes.Add(DebuggerMessageType.UnityTest);
            Debugger.EnabledTypes.Add(RMDebugMessage.WorldCreation);
            Debugger.EnabledTypes.Add(RMDebugMessage.Logic);
            Debugger.EnabledTypes.Add(RMDebugMessage.Workflow);
            Debugger.EnabledTypes.Add(DebuggerMessageType.Workflow);
            Debugger.Logger = s => Debug.Log(s);

            Loader = new Loader();
            Loader.AddLevel("Demo", "Test", () => new DemoCompetitions.Level1());
            Loader.AddLevel("RoboMovies", "Test", () => new RMCompetitions.Level1());

            server = new PercistentTCPServer(14000);
            server.ClientConnected += ClientConnected;
            server.Printer = str => Debugger.Log(DebuggerMessageType.Unity, "FROM SERVER: " + str);
            server.StartThread();
        }

        static void ClientConnected(CvarcClient client)//это можно засунуть прямо в ТСП сервер!
        {
            Queue.Enqueue(new NetworkRunner(client));
        }

        static public void IntroductionTick()
        {
            if (Queue.Count != 0 && (currentRunner == null || currentRunner.CanInterrupt))
            {

            }
        }

        static public void RoundTick()
        {

        }

        public static void Exited()
        {
            //Application.LoadLevel("Intro"); добавить ее как только положим в нужный неймспейс?
            Debugger.Log(DebuggerMessageType.Unity, "local exit");
            server.RequestExit();
        }
    }
}
