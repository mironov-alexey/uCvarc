using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CVARC.V2;
using RoboMovies;

namespace TournamentTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(ToolConstants.FileWithPlan);
            var games = lines.Select(x => x.Split(':'));
            listener = StartTcpListener();
            var results = games
                .Select(game => RunGame(game[0], game[1], game[2], game.Length > 3 ? game[3] : null))
                .ToList();
            File.WriteAllLines("Results.txt", results);
        }

        static TcpListener listener;

        static string RunGame(string player1, string player2, string tag, string subtag = null)
        {
            InitConnection();
            var player1Process = RunPlayer(player1);
            if (!IsPlayerConnected())
            {
                if (!player1Process.HasExited)
                    player1Process.Kill();
                return "Cant play game, " + player1 + " is not connected";
            }
            Console.WriteLine("Unity accepted client " + player1);
            var player2Process = RunPlayer(player2);
            if (!IsPlayerConnected())
            {
                if (!player1Process.HasExited)
                    player1Process.Kill();
                if (!player2Process.HasExited)
                    player2Process.Kill();
                return "Cant play game, " + player2 + " is not connected";
            }
            Console.WriteLine("Unity accepted client " + player2);

            var answer = AcceptResultString(int.MaxValue); // т.к. мы доверяем юнити, ждем ДОЛГО.
            if (!player1Process.HasExited)
                player1Process.Kill();
            if (!player2Process.HasExited)
                player2Process.Kill();
            Console.WriteLine("Game played. results saved");
            return string.IsNullOrEmpty(answer)
                ? "Cant play game " + player1 + " vs " + player2
                : answer + ":" + tag + ":" + subtag;
        }

        static bool IsPlayerConnected()
        {
            // если юнити "что-то" нам отправила -- значит игрок подключился.
            return !string.IsNullOrEmpty(AcceptResultString(1000 * 1000)); // даем одну секунду на то, чтоб юнити прислала нам "что-то"
        }

        static TcpListener StartTcpListener()
        {
            var listener = new TcpListener(IPAddress.Any, ToolConstants.MyPort);
            Action action = listener.Start;
            action.BeginInvoke(null, null);
            return listener;
        }

        static string AcceptResultString(int timeoutInMicroseconds)
        {
            if (!listener.Server.Poll(timeoutInMicroseconds, SelectMode.SelectRead)) // сколько ждать в микросекундах?
                return null;
            using (var client = listener.AcceptTcpClient())
            {
                var stream = client.GetStream();
                var bytes = new byte[client.ReceiveBufferSize];
                var bytesRecieved = stream.Read(bytes, 0, client.ReceiveBufferSize);
                return Encoding.ASCII.GetString(bytes, 0, bytesRecieved);
            }
        }

        static void InitConnection()
        {
            var client = new TestingClient();
            var configs = new ConfigurationProposal
            {
                LoadingData = new LoadingData { AssemblyName = ToolConstants.AssemblyName, Level = ToolConstants.Level },
                SettingsProposal = ToolConstants.Settings
            };
            try
            {
                client.Configurate(ToolConstants.ServicePort, configs, ToolConstants.WorldState, ToolConstants.Ip);
            }
            catch (Exception e)
            {
                Console.WriteLine("maybe error while sending settings: " + e.Message);
            }
            client.Exit();
        }

        static Process RunPlayer(string playerName)
        {
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = false, // temp ?
                UseShellExecute = false,
                FileName = ToolConstants.DirectoryWithPlayers + playerName + "/" +
                           ToolConstants.NameOfExe,
                Arguments = ToolConstants.Ip + " " + ToolConstants.PlayPort
            };
            Console.WriteLine("player started: " + startInfo.FileName);
            return Process.Start(startInfo);
        }
    }
}
