using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Tools;
using CVARC.V2;

namespace Assets
{
    public class TournamentRunner : IRunner
    {
        readonly List<TournamentPlayer> players;
        readonly int requiredCountOfPlayers;
        readonly Configuration configuration;
        readonly string[] controllerIds;
        readonly IWorldState worldState;
        readonly string logFileName;
        NetTournamentControllerFactory factory;

        public IWorld World { get; set; }
        public string Name { get; set; }
        public bool CanStart { get; set; }
        public bool CanInterrupt { get; set; }


        public TournamentRunner(LoadingData loadingData, IWorldState worldState)
        {
            this.worldState = worldState;
            players = new List<TournamentPlayer>();

            var competitions = Dispatcher.Loader.GetCompetitions(loadingData);
            var settings = competitions.Logic.CreateDefaultSettings();
            configuration = new Configuration
            {
                LoadingData = loadingData,
                Settings = settings
            };

            //я игнорирую конфиги. надо хотя бы имя сохранять в метод "add player"
            // wut?

            //log section
            logFileName = Guid.NewGuid() + ".log";
            configuration.Settings.EnableLog = true;
            configuration.Settings.LogFile = UnityConstants.LogFolderRoot + logFileName;
            //log section end

            controllerIds = competitions.Logic.Actors.Keys.ToArray();

            foreach (var controller in controllerIds
                .Select(x => new ControllerSettings
                {
                    ControllerId = x,
                    Type = ControllerType.Client,
                    Name = settings.Name
                }))
                settings.Controllers.Add(controller);

            requiredCountOfPlayers = controllerIds.Length;

            Debugger.Log(DebuggerMessageType.Unity, "t.runner created. count: " + requiredCountOfPlayers);
            if (requiredCountOfPlayers == 0)
                throw new Exception("requiered count of players cant be 0");

            Name = loadingData.AssemblyName + loadingData.Level;//"Tournament";
            CanInterrupt = false;
            CanStart = false;
        }

        public bool AddPlayerAndCheck(TournamentPlayer player)
        {
            if (players.Count == requiredCountOfPlayers)
                throw new Exception("already started");
            players.Add(player);
            if (players.Count != requiredCountOfPlayers) 
                return false;
            PrepareStart();
            return true;
        }

        void PrepareStart()
        {
            var controllersMap = new Dictionary<string, IMessagingClient>();
            for (var i = 0; i < requiredCountOfPlayers; i++)
                controllersMap.Add(controllerIds[i], players[i].client);
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
            Debugger.Log(DebuggerMessageType.Unity, "dispose tournament...");
            foreach (var cvarcClient in players)
                cvarcClient.client.Close();

            if (World != null)
                World.OnExit();

            SendResultsToServer();
        }

        private void SendResultsToServer()
        {
            if (!UnityConstants.NeedToSendToWeb || World == null) 
                return;
            var leftTag = players[controllerIds.ToList().IndexOf("Left")].configProposal.SettingsProposal.CvarcTag; // не знаю, как определить который из них левый.
            var rightTag = players[controllerIds.ToList().IndexOf("Right")].configProposal.SettingsProposal.CvarcTag; // вроде так.
            var scores = World.Scores.GetAllScores();
            var leftScore = -1;
            var rightScore = -1;
            foreach (var scoresInfo in scores)
            {
                if (scoresInfo.Item1 == "Left")
                    leftScore = scoresInfo.Item2;
                if (scoresInfo.Item1 == "Right")
                    rightScore = scoresInfo.Item2;
            }
            HttpWorker.SendGameResults(leftTag, rightTag, leftScore, rightScore, logFileName);
        }
    }
}
