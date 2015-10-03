﻿using System;
using System.Threading;

namespace CVARC.V2
{
    /// <summary>
    /// This class contains various information about server and the created world. Loader methods fills the properties of this class step-by-step, thus initializing the world.
    /// It is typically used to share the information between the server and the client in case when both are started within the same process. Unit test client is an example.
    /// It is also used to configure the server side.
    /// </summary>
    public class NetworkServerData : INetworkData
    {
        /// <summary>
        /// The port on which the server is operating
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// True when Listener is up
        /// </summary>
        public bool ServerLoaded { get; set; }

        /// <summary>
        /// The connection on server that waits for commands
        /// </summary>
        public IMessagingClient ClientOnServerSide { get; set; }


        /// <summary>
        /// Data about which competitions to run
        /// </summary>
        public LoadingData LoadingData { get; set; }

        /// <summary>
        /// The resulting settings
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// The initial state of the world that was sent by the client.
        /// </summary>
        public IWorldState WorldState { get; set; }

        /// <summary>
        /// The world that was created
        /// </summary>
        public IWorld World { get; set; }

        /// <summary>
        /// The call-back to stop the server.
        /// </summary>
        public Action StopServer { get; set; }

        /// <summary>
        /// Closes the client-server connection from client side.
        /// </summary>
        public void Close()
        {
            if (!ServerLoaded) return;
            if (StopServer != null)
                StopServer();
            ServerLoaded = false;
        }

        /// <summary>
        /// Waits until server is ready. It is typically used for client thread to wait until the server opens the port.
        /// </summary>
        public void WaitForServer()
        {
            while (!ServerLoaded) Thread.Sleep(1);
        }

        /// <summary>
        /// Waits until the world is completely initialized. It is typically used for client thread to wait until it may start the interaction.
        /// </summary>
        /// <returns></returns>
        public IWorld WaitForWorld()
        {
            while (World == null) Thread.Sleep(1);
            return World;
        }
    }
}
