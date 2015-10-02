using CVARC.V2;


namespace Assets
{ //LogPlayerController CVARC.Unity.sln
    public class LogRunner : IRunner
    {
        public string Name { get; private set; }
        public IWorld World { get; private set; }
        public bool CanStart { get; private set; }
        public bool CanInterrupt { get; private set; }
        readonly Log log;
        readonly Configuration configuration;
        readonly IWorldState worldState;
        readonly LogPlayerControllerFactory factory;

        public LogRunner(string fileName)
        {
            log = Log.Load(UnityConstants.LogFolderRoot + fileName);
            factory = new LogPlayerControllerFactory(log);
            configuration = log.Configuration;
            configuration.Settings.EnableLog = false; // чтоб файл логов не переписывать

            worldState = log.WorldState;

            Name = "log run: " + fileName;
            CanInterrupt = true;
            CanStart = true;
        }

        public void InitializeWorld()
        {
            if (World != null)
                return;
            World = Dispatcher.Loader.CreateWorld(configuration, factory, worldState);
        }

        public void Dispose()
        {
            World.OnExit();
        }
    }
}
