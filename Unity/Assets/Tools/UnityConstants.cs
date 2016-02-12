using System.Linq;
using CVARC.V2;

namespace Assets
{
    public static class UnityConstants
    {
        public const int SoloNetworkPort = 14000;
        public const int TournamentPort = 14001;
        public const int ServicePort = 14002;
        public const bool NeedToOpenServicePort = true;
        public const bool OnlyGamesThroughServicePort = true;
        public const int PortToSendTcpResults = 14500;
        public const string LogFolderRoot = "GameLogs/";
        public const string PathToConfigFile = "config&key.txt";
    }
}
