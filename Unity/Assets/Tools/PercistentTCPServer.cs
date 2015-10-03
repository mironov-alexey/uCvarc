using System.Net;
using System.Net.Sockets;
using System.Threading;
using CVARC.V2;


namespace Assets
{
    public class PercistentTCPServer
    {
        readonly int port;
        readonly bool tournamentMode;
        bool exitRequest;

        public PercistentTCPServer(int port, bool tournamentMode)
        {
            this.port = port;
            this.tournamentMode = tournamentMode;
        }

        void Print(string str)
        {
             Debugger.Log(DebuggerMessageType.Unity, (tournamentMode ? "tournament server: " : "network server: ") + str);
        }

        public void RequestExit()
        {
            exitRequest = true;
        }

        public void StartThread()
        {
            Print("Server started");
            var listner = new TcpListener(IPAddress.Any, port);
            listner.Start();
            while (true)
            {
                while (!listner.Pending())
                {
                    if (exitRequest)
                    {
                        listner.Stop();
                        Print("Server Exited");
                        return;
                    }
                    Thread.Sleep(1);
                }
                var client = listner.AcceptTcpClient();
                Print("Client accepted");
                var cvarcClient = new CvarcClient(client);
                if (tournamentMode)
                    TournamentPool.AddPlayerToPool(cvarcClient);
                else
                    Dispatcher.AddRunner(new NetworkRunner(cvarcClient));
            }
        }
    }
}
