﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CVARC.V2
{
	public class CvarcClient<TSensorData, TCommand>
		where TSensorData : class
	{
		CvarcClient client;

		public static void StartKrorServer(int port)
		{
			var process = new Process();
			process.StartInfo.FileName = "CVARC.exe";
			process.StartInfo.Arguments = "Debug " + port.ToString();
			process.Start();
			Thread.Sleep(100);
		}

		public TSensorData Configurate(int port, ConfigurationProposal configuration, IWorldState state, string ip = "127.0.0.1")
		{
			var tcpClient = new TcpClient();
			tcpClient.Connect(ip, port);
			client = new CvarcClient(tcpClient);
			client.Write(configuration);
			client.Write(state);
            var sensorData=client.Read<TSensorData>();
			OnSensorDataReceived(sensorData);
			return sensorData;
		}



		public TSensorData Act(TCommand command)
		{
			client.Write(command);
			var sensorData = client.Read<TSensorData>(); // 11!!!
			//if (sensorData == null)
			//	Environment.Exit(0);
				OnSensorDataReceived(sensorData);
			return sensorData;
		}

		public event Action<TSensorData> SensorDataReceived;
		void OnSensorDataReceived(TSensorData sensorData)
		{
			if (SensorDataReceived!=null)
				SensorDataReceived(sensorData);
		}

		public void Exit()
		{
			client.Close();
		}
	}
}
