using CVARC.V2;

namespace Assets.Temp
{
    public class NetworkRunner : IRunner
    {
        readonly CvarcClient client;
        IWorld world;

        public NetworkRunner(CvarcClient client)
        {
            this.client = client;
            CanStart = true;
            CanInterrupt = true;
        }

        public IWorld CreateWorld()
        {
            var waitingNetworkServer = new NetworkServerData { Port = 14000 };
            waitingNetworkServer.ServerLoaded = true;
            waitingNetworkServer.ClientOnServerSide = client;
            Dispatcher.Loader.ReceiveConfiguration(waitingNetworkServer);
            Dispatcher.Loader.InstantiateWorld(waitingNetworkServer);
            var world = waitingNetworkServer.World;
            world.Exit += Dispatcher.Exited;
            return world;
        }

        public bool CanStart {get; private set;}

        public bool CanInterrupt {get; private set;}

        public void Dispose()
        {
            client.Close();
        }
    }
}
