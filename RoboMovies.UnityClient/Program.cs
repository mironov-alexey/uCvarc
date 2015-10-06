﻿namespace RoboMovies.UnityClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // создаем клиента
            var client = new Level1Client();
            // назначаем обработчик сенсоров
            client.SensorDataReceived += HandleSensorData;
            // указываем настройки
            client.Configurate(14000, true, RoboMoviesBots.Stand, ip:"127.0.0.1");
            Control(client);
        }

        static void Control(RMClient<FullMapSensorData> client)
        {
            // управляем роботом
            client.Move(65);
            client.Rotate(-90);
            client.Stand(1);
            client.GripPopCorn();
            client.Rotate(-90);
            client.Move(70);
            client.Stand(1);
            client.ReleasePopCorn();

            //корректно завершаем работу
            client.Exit();
        }

        static void HandleSensorData(FullMapSensorData sensorData)
        {
            
        }
    }
}
