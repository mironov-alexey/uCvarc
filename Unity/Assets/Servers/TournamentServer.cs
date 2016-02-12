﻿using CVARC.V2;


namespace Assets
{
    class TournamentServer : UnityServer
    {
        public TournamentServer(int port) : base(port) { }

        protected override void HandleClient(CvarcClient client)
        {
            
            var configProposal = client.Read<ConfigurationProposal>();
            var loadingData = configProposal.LoadingData; //RoboMoviesLevel1
            var competitions = Dispatcher.Loader.GetCompetitions(loadingData);
            var worldSettingsType = competitions.Logic.WorldStateType;
            var worldState = (IWorldState)client.Read(worldSettingsType);
            var settings = competitions.Logic.CreateDefaultSettings(); // таким образом игнорируются все настйроки пользователя сейчас.

            var configuration = new Configuration
            {
                LoadingData = loadingData,
                Settings = settings
            };
            TournamentPool.AddPlayerToPool(client, configuration, worldState, configProposal);
        }

        protected override void Print(string str)
        {
            Debugger.Log(DebuggerMessageType.Unity, "tournament server: " + str);
        }
    }
}
