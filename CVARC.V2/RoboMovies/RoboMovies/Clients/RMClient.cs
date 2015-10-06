using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIRLab.Mathematics;
using CVARC.V2;

namespace RoboMovies
{
    public abstract class RMClient<TSensorData> : CvarcClient<TSensorData, RMCommand>
        where TSensorData : class
    {
        public abstract string LevelName { get; }

        public TSensorData Configurate(int port, bool isOnLeftSide, 
            RoboMoviesBots bot = RoboMoviesBots.Stand, string ip = "127.0.0.1")
        {
            var configuration = new ConfigurationProposal();
            configuration.LoadingData.AssemblyName = "RoboMovies";
            configuration.LoadingData.Level = LevelName;
            configuration.SettingsProposal.Controllers = new List<ControllerSettings>();
            configuration.SettingsProposal.Controllers.Add(new ControllerSettings
            {
                ControllerId = isOnLeftSide ? TwoPlayersId.Left : TwoPlayersId.Right,
                Name = "This",
                Type = ControllerType.Client
            });
            configuration.SettingsProposal.Controllers.Add(new ControllerSettings
            {
                ControllerId = isOnLeftSide ? TwoPlayersId.Right : TwoPlayersId.Left,
                Name = bot.ToString(),
                Type = ControllerType.Bot
            });


            return Configurate(port, configuration, new RMWorldState(), ip );
        }

        public TSensorData Move(double distance)
        {
            return Act(RMRules.Current.Move(distance));
        }

        public TSensorData Rotate(double angleInGrad)
        {
            return Act(RMRules.Current.Rotate(Angle.FromGrad(angleInGrad)));
        }

        public TSensorData CollectDetail()
        {
            return Act(RMRules.Current.Collect());
        }

		public TSensorData BuildTower()
		{
			return Act(RMRules.Current.BuildTower());
		}

        public TSensorData GripPopCorn()
        {
            return Act(RMRules.Current.Grip());
        }

        public TSensorData CloseClapperBoardLeft()
        {
            return Act(RMRules.Current.UseLeftDeployer());
        }

        public TSensorData CloseClapperBoardRight()
        {
            return Act(RMRules.Current.UseRightDeployer());
        }

        public TSensorData WalkUpstairs()
        {
            return Act(RMRules.Current.UpLadder());
        }

        public TSensorData ReleasePopCorn()
        {
            return Act(RMRules.Current.Release());
        }

        public TSensorData ActivatePopCornDispenser()
        {
            return Act(RMRules.Current.GetPopCorn());
        }

        public void Stand(double time)
        {
            Act(RMRules.Current.Stand(time));
        }

    }

}
