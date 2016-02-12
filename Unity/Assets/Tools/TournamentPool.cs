using System.Collections.Generic;
using System.Threading;
using Assets;
using Assets.Tools;
using CVARC.V2;

namespace Assets
{
    public static class TournamentPool
    {
        static readonly Dictionary<LoadingData, TournamentRunner> pool = new Dictionary<LoadingData, TournamentRunner>();
        static bool haveForceGame;
        static TournamentRunner forceRunner;


        public static void AddPlayerToPool(CvarcClient client, Configuration configuration, IWorldState worldState, ConfigurationProposal configProposal)
        {
            if (haveForceGame)
            {
                if (UnityConstants.OnlyGamesThroughServicePort)
                    HttpWorker.SendInfoToLocal(configProposal.SettingsProposal.CvarcTag);
                if (forceRunner.AddPlayerAndCheck(new TournamentPlayer(client, configProposal, worldState)))
                    haveForceGame = false;
                return;
            }
            lock (pool)
            {
                if (!pool.ContainsKey(configuration.LoadingData))
                {
                    pool.Add(configuration.LoadingData, new TournamentRunner(worldState, configuration));
                    Dispatcher.AddRunner(pool[configuration.LoadingData]);
                }
                if (pool[configuration.LoadingData].AddPlayerAndCheck(new TournamentPlayer(client, configProposal, worldState)))
                    pool.Remove(configuration.LoadingData);
            }
        }

        public static void AddForceGame(IWorldState worldState, Configuration config)
        {
            if (forceRunner != null)
                while (!forceRunner.Disposed)
                    Thread.Sleep(100);
            haveForceGame = true;
            lock (pool)
                pool.Clear();
            forceRunner = new TournamentRunner(worldState, config);
            Dispatcher.AddRunner(forceRunner);
        }
    }
}
