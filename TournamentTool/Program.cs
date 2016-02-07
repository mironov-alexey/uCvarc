using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using CVARC.V2;
using RoboMovies;

namespace TournamentTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var results = new List<string>();
            var lines = File.ReadAllLines(ToolConstants.FileWithPlan);
            var games = lines.Select(x => x.Split(':'));
            var listener = StartTcpListener();
            foreach (var game in games)
            {
                RunGame(game[0], game[1]);
                var res = AcceptResultString(listener);
                results.Add(string.IsNullOrEmpty(res) ? 
                    "Cant play game " + game[0] + " vs " + game[1] : res + ":" + game[2] + ":" + game[3]);
            }
            File.WriteAllLines("Results.txt", results);
        }

        static void RunGame(string player1, string player2)
        {
            InitConnection();
            var player1Process = RunPlayer(player1);
            var player2Process = RunPlayer(player2);
            var sleeped = 0;
            while (sleeped < 100 && (!player1Process.HasExited || !player2Process.HasExited))
            {
                Thread.Sleep(100);
                sleeped++;
            }
            if (!player1Process.HasExited)
                player1Process.Kill();
            if (!player2Process.HasExited)
                player2Process.Kill();
        }

        static TcpListener StartTcpListener()
        {
            var listener = new TcpListener(IPAddress.Any, ToolConstants.MyPort);
            Action action = listener.Start;
            action.BeginInvoke(null, null);
            return listener;
        }

        static string AcceptResultString(TcpListener listener)
        {
            if (!listener.Server.Poll(1000*1000*2, SelectMode.SelectRead)) // сколько ждать в микросекундах?
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
            var client = new Level1Client();
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
                Console.WriteLine(e.Message);
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
            Console.WriteLine(startInfo.FileName);
            return Process.Start(startInfo);
        }
    }
}
