namespace Assets
{
    public static class UnityConstants
    {
        public const int SoloNetworkPort = 14000;
        public const int TournamentPort = 14001;
        public const string LogFolderRoot = "GameLogs/";
        public const string WebIp = "localhost";
        public const int WebPort = 63895;
        public const bool NeedToSendToWeb = true;
        public const string Method = "Competition/SendResult";
        public const string PasswordToWeb = "somePassword"; // top defence ever.
    }
}
