using System;
using AIRLab.Mathematics;

namespace RoboMovies.UnityClient
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
            client.Configurate(14000, true, RoboMoviesBots.Stand, ip: "127.0.0.1", cvarcTag: "00000000-0000-0000-0000-000000000000");
            Control(client);
        }

        static void Control(RMClient<FullMapSensorData> rules)
        {
            try
            {
                rules.Move(115);
                rules.Rotate(90);
                rules.Move(-58);
                rules.Stand(0.1);
                rules.Grip();
                rules.Move(60);
                rules.Rotate(-90);
                rules.Move(-115);
                rules.Stand(0.1);
                rules.Release();
            }
            catch (Exception e)
            {
                Console.WriteLine("maybe erorr on client side: " + e.Message);
            }
            //корректно завершаем работу
            rules.Exit();
        }

        static void HandleSensorData(FullMapSensorData sensorData)
        {
            
        }
    }
}
