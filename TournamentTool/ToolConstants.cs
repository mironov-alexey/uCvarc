using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;
using RoboMovies;

namespace TournamentTool
{
    public class ToolConstants
    {
        /* format of file:
         * %playername%:%playername%:tag:subtag
         * next line...
         * */
        public const string FileWithPlan = "plan.txt";
        public const string DirectoryWithPlayers = "Players/"; // экзешник должен находиться в %playername%/NameOfExe
        public const string NameOfExe = "RoboMovies.UnityClient.exe";
        public const string AssemblyName = "RoboMovies";
        public const string Level = "Level1";
        public const string Ip = "127.0.0.1";
        public const int PlayPort = 14001;
        public const int ServicePort = 14002;
        public const int MyPort = 14500;
        public static readonly SettingsProposal Settings = new SettingsProposal {OperationalTimeLimit = 5, TimeLimit = 90};
        public static readonly IWorldState WorldState = new RMWorldState {Seed = 0};
    }
}
