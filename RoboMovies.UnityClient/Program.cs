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
            if (args.Length > 1)
                client.Configurate(int.Parse(args[1]), true, RoboMoviesBots.Stand, ip: args[0], cvarcTag: "00000000-0000-0000-0000-000000000008");
            else
                client.Configurate(14000, false, RoboMoviesBots.Stand, ip: "127.0.0.1", cvarcTag: "00000000-0000-0000-0000-000000000008");
            Control(client);
        }

        static int pos = 0;

        static void Control(RMClient<FullMapSensorData> rules)
        {
            try
            {
                if (pos == 2)
                {
                    rules.Move(64);
                    rules.Rotate(-90);
                    rules.Stand(0.1);
                    rules.Grip();
                    rules.Rotate(90);
                    rules.Move(-54);
                    rules.Stand(0.1);
                    rules.Release();
                }
                else
                {
                    rules.Move(64);
                    rules.Rotate(90);
                    rules.Stand(0.1);
                    rules.Grip();
                    rules.Rotate(-90);
                    rules.Move(-54);
                    rules.Stand(0.1);
                    rules.Release();
                }


                //bug way
                //while (true)
                //{
                //    for (var i = 0; i < 5; i++)
                //        rules.Move(5);
                //    for (var i = 0; i < 5; i++)
                //        rules.Move(-5);
                //}
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
            if (pos == 0)
                pos = sensorData.SelfLocation.X > 10 ? 1 : 2;
            // pos 2 -- left. pos 1 -- right.
        }
    }
}
