using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;

namespace Assets.Servers
{
    class ServiceServer : UnityServer
    {
        public ServiceServer(int port) : base(port) { }

        protected override void HandleClient(CvarcClient client)
        {
            var configProposal = client.Read<ConfigurationProposal>();
            var loadingData = configProposal.LoadingData; //RoboMoviesLevel1
            var competitions = Dispatcher.Loader.GetCompetitions(loadingData);
            var worldSettingsType = competitions.Logic.WorldStateType;
            var worldState = (IWorldState)client.Read(worldSettingsType);
            var settings = competitions.Logic.CreateDefaultSettings();
            if (configProposal.SettingsProposal != null)
                configProposal.SettingsProposal.Push(settings, true);
            var configuration = new Configuration
            {
                LoadingData = loadingData,
                Settings = settings
            };

            TournamentPool.AddForceGame(worldState, configuration);

            client.Close();
        }

        protected override void Print(string str)
        {
            Debugger.Log(DebuggerMessageType.Unity, "Service server: " + str);
        }
    }
}
