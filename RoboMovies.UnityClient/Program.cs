using System;

namespace RoboMovies.UnityClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // создаем клиента
            var client = new TestingClient();
            // назначаем обработчик сенсоров
            client.SensorDataReceived += HandleSensorData;
            // указываем настройки
            client.Configurate(14001, false, RoboMoviesBots.Stand, ip: "127.0.0.1", cvarcTag: "00000000-0000-0000-0000-000000000000");
            Control(client);
        }

        static void Control(RMClient<RMSensorData> client)
        {
            // управляем роботом
            //client.Move(65);
            //client.Rotate(-90);
            //client.Stand(1);
            //client.GripPopCorn();
            //client.Rotate(-90);
            //client.Move(8);

            //client.Rotate(-90);
            //client.Stand(0.5);
            //client.Move(-85);
            //client.Stand(1);
            //client.ActivatePopCornDispenser();
            //client.Move(70);
            //client.Rotate(90);
            //client.Move(10);
            //client.ReleasePopCorn();

            client.Move(64);
            client.Rotate(-90);
            client.Stand(0.1);
            client.GripPopCorn();
            client.Rotate(90);
            client.Move(-30);
            client.Rotate(-90);
            var firstPopCornCount = client.Move(-70).LoadedPopCornCount;
            client.Stand(0.1);
            var secondPopCornCount = client.ActivatePopCornDispenser().LoadedPopCornCount;

            //проверяем, что количество попкорна увеличилось на 1
            System.Diagnostics.Debug.Assert(secondPopCornCount - firstPopCornCount == 1);

            //корректно завершаем работу
            client.Exit();
        }

        static void HandleSensorData(RMSensorData sensorData)
        {

        }
    }
}
