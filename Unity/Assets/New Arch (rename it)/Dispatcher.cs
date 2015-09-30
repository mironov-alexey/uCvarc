using System;
using System.Collections.Generic;
using CVARC.V2;
using RoboMovies;
using UnityEngine;

namespace Assets.Temp
{
    //todo: добавить локи на очередь везде.
    public static class Dispatcher
    {
        public static Loader Loader { get; private set; }
        public static readonly Queue<IRunner> Queue = new Queue<IRunner>();
        public static bool IsGameOver;
        static PercistentTCPServer server;
        public static IRunner currentRunner { get; private set; }

        public static void Start()
        {
            Debugger.DisableByDefault = true;
            Debugger.EnabledTypes.Add(DebuggerMessageType.Unity);
            Debugger.EnabledTypes.Add(DebuggerMessageType.UnityTest);
            Debugger.EnabledTypes.Add(RMDebugMessage.WorldCreation);
            Debugger.EnabledTypes.Add(RMDebugMessage.Logic);
            Debugger.EnabledTypes.Add(RMDebugMessage.Workflow);
            Debugger.EnabledTypes.Add(DebuggerMessageType.Workflow);
            Debugger.Logger = Debug.Log;

            Loader = new Loader();
            Loader.AddLevel("Demo", "Test", () => new DemoCompetitions.Level1());
            Loader.AddLevel("RoboMovies", "Test", () => new RMCompetitions.Level1());

            server = new PercistentTCPServer(14000);
            server.ClientConnected += ClientConnected;
            server.Printer = str => Debugger.Log(DebuggerMessageType.Unity, "FROM SERVER: " + str);
            server.StartThread();
        }

        static void ClientConnected(CvarcClient client)
        {
            //временно. тут нужно бы определить, какой раннер создавать.
            //или, если тсп сервер используется только для создания нетворкРаннера, 
            //запихать это прямо туд и отказаться от использования события.
            Queue.Enqueue(new NetworkRunner(client));
        }

        public static void IntroductionTick()
        {
            if (Queue.Count != 0)
            {
                Application.LoadLevel("Round");
            }
        }

        public static void RoundTick()
        {
            // конец игры
            if (IsGameOver && currentRunner != null)
            {
                currentRunner.Dispose();
                currentRunner = null;
            }

            if (currentRunner == null)
            {
                // начало игры
                if (Queue.Count != 0)
                {
                    currentRunner = Queue.Dequeue();
                    IsGameOver = false;
                    currentRunner.CreateWorld(); // Посмотреть, будет ли где-то использоваться эта функция как не void
                }
                // возврат
                else
                    Application.LoadLevel("Intro");
            }
        }

        // самый ГЛОБАЛЬНЫЙ выход, из всей юнити. Юзать один раз, как и Start
        public static void OnDispose()
        {
            server.RequestExit();
            if (currentRunner != null)
                currentRunner.Dispose();
            foreach (var runner in Queue)
                runner.Dispose();

            TestDispatcher.OnDispose();
        }
    }
}
