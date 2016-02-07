using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;
using RoboMovies;

namespace TournamentTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Level1Client();
            var settings = new SettingsProposal {OperationalTimeLimit = 5, TimeLimit = 90};
            var configs = new ConfigurationProposal
            {
                LoadingData = new LoadingData() {AssemblyName = "RoboMovies", Level = "Level1"},
                SettingsProposal = settings
            };
            try
            {
                client.Configurate(14002, configs, new RMWorldState { Seed = 0 }, "127.0.0.1");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            client.Exit();
        }
    }
}
