using System.Collections.Generic;
using System.Threading;
using Assets;
using CVARC.V2;

namespace Assets
{
    public static class TournamentPool
    {
        static readonly Dictionary<LoadingData, TournamentRunner> pool = new Dictionary<LoadingData, TournamentRunner>();
        static bool haveForceGame;
        static TournamentRunner forceRunner;


        public static void AddPlayerToPool(CvarcClient client, LoadingData loadingData, Configuration configuration, IWorldState worldState, ConfigurationProposal configProposal)
        {
            if (haveForceGame)
            {
                if (forceRunner.AddPlayerAndCheck(new TournamentPlayer(client, configProposal, worldState)))
                    haveForceGame = false;
                return;
            }
            lock (pool)
            {
                if (!pool.ContainsKey(loadingData))
                {
                    pool.Add(loadingData, new TournamentRunner(loadingData, worldState, configuration));
                    Dispatcher.AddRunner(pool[loadingData]);
                }
                if (pool[loadingData].AddPlayerAndCheck(new TournamentPlayer(client, configProposal, worldState)))
                    pool.Remove(loadingData);
            }
        }

        public static void AddForceGame(LoadingData loadingData, IWorldState worldState, Configuration config)
        {
            if (forceRunner != null)
                while (!forceRunner.Disposed)
                    Thread.Sleep(100);
            haveForceGame = true;
            lock (pool)
                pool.Clear();
            forceRunner = new TournamentRunner(loadingData, worldState, config);
        }
    }
}
