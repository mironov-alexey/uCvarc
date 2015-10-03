using System;
using System.Collections.Generic;
using System.Linq;
using CVARC.V2;

namespace Assets
{
    public class TournamentRunner : IRunner
    {
        readonly List<CvarcClient> players;
        readonly int requiredCountOfPlayers;
        readonly Configuration configuration;
        readonly string[] controllerIds;
        readonly IWorldState worldState;
        NetTournamentControllerFactory factory;

        public IWorld World { get; set; }
        public string Name { get; set; }
        public bool CanStart { get; set; }
        public bool CanInterrupt { get; set; }


        public TournamentRunner(LoadingData loadingData, IWorldState worldState)
        {
            this.worldState = worldState;
            players = new List<CvarcClient>();

            var competitions = Dispatcher.Loader.GetCompetitions(loadingData);
            var settings = competitions.Logic.CreateDefaultSettings();
            configuration = new Configuration
            {
                LoadingData = loadingData,
                Settings = settings
            };

            controllerIds = competitions.Logic.Actors.Keys.ToArray();
            requiredCountOfPlayers = controllerIds.Length;

            Debugger.Log(DebuggerMessageType.Unity, "t.runner created. count: " + requiredCountOfPlayers);
            if (requiredCountOfPlayers == 0)
                throw new Exception("requiered count of players cant be 0");

            Name = loadingData.AssemblyName + loadingData.Level;//"Tournament";
            CanInterrupt = false;
            CanStart = false;
        }

        public bool AddPlayerAndCheck(CvarcClient client)
        {
            if (players.Count == requiredCountOfPlayers)
                throw new Exception("already started");
            players.Add(client);
            if (players.Count != requiredCountOfPlayers) 
                return false;
            PrepareStart();
            return true;
        }

        void PrepareStart()
        {
            var controllersMap = new Dictionary<string, IMessagingClient>();
            for (var i = 0; i < requiredCountOfPlayers; i++)
                controllersMap.Add(controllerIds[i], players[i]);
            factory = new NetTournamentControllerFactory(controllersMap);

            CanStart = true;
        }

        public void InitializeWorld()
        {
            if (World == null)
                World = Dispatcher.Loader.CreateWorld(configuration, factory, worldState);
        }

        public void Dispose()
        {
            foreach (var cvarcClient in players)
                cvarcClient.Close();

            if (World != null)
                World.OnExit();
        }
    }
}
