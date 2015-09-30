using System.Collections.Generic;
using System.Threading;
using CVARC.V2;
using RoboMovies;
using UnityEngine;
using Assets;


public static class Dispatcher
{
    public static Loader Loader { get; private set; }
    public static readonly Queue<IRunner> Queue = new Queue<IRunner>();
    public static IRunner currentRunner { get; private set; }
    static bool IsGameOver;
    static PercistentTCPServer server;
    static bool switchingScenes;

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
        new Thread(() => server.StartThread()).Start();
    }

    static void ClientConnected(CvarcClient client)
    {
        //временно. тут нужно бы определить, какой раннер создавать.
        //или, если тсп сервер используется только для создания нетворкРаннера, 
        //запихать это прямо тудa и отказаться от использования события.
        lock (Queue)
        {
            Queue.Enqueue(new NetworkRunner(client));
        }
    }

    static void SwitchScene(string sceneName)
    {
        switchingScenes = true;
        Application.LoadLevel(sceneName);
    }

    public static void IntroductionTick()
    {
        if (Queue.Count != 0) // && can start!!!
            SwitchScene("Round");
    }

    public static void RoundStart()
    {
        lock (Queue)
            currentRunner = Queue.Dequeue();
        IsGameOver = false;
        currentRunner.CreateWorld(); // Посмотреть, будет ли где-то использоваться эта функция как не void
    }

    public static void RoundTick()
    {
        // конец игры
        if (IsGameOver && currentRunner != null)
        {
            Debug.Log("game over. disposing");
            currentRunner.Dispose();
            currentRunner = null;
        }

        if (currentRunner == null)
        {
            if (Queue.Count != 0)
            {
                // Это "очищение" сцены с помощью переключения на нее же.
                SwitchScene("Round");
                return;
            }
            // возврат
            SwitchScene("Intro");
        }

        if (Queue.Count != 0 && currentRunner.CanInterrupt)
            SetGameOver();
    }

    // самый ГЛОБАЛЬНЫЙ выход, из всей юнити. Вызывается из сцен.
    public static void OnDispose()
    {
        if (switchingScenes)
        {
            switchingScenes = false;
            return;
        }

        Debug.Log("GLOBAL EXIT");
        server.RequestExit();
        if (currentRunner != null)
            currentRunner.Dispose();
        lock (Queue)
        {
            foreach (var runner in Queue)
                runner.Dispose();
        }

        TestDispatcher.OnDispose();
    }

    public static void SetGameOver()
    {
        IsGameOver = true;
    }
}
