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
            client.Configurate(14000, true, RoboMoviesBots.Stand, ip: "127.0.0.1");
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
            client.Move(8);

            client.Rotate(-90);
            client.Stand(0.5);
            client.Move(-85);
            client.Stand(1);
            client.ActivatePopCornDispenser();
            client.Move(70);
            client.Rotate(90);
            client.Move(10);
            client.ReleasePopCorn();
            client.Move(-40);
            //client.Move(64);
            //client.Rotate(-90);
            //client.Stand(0.1);
            //client.GripPopCorn();
            //client.Rotate(90);
            //client.Move(-30);
            //client.Rotate(-90);
            //client.Move(-70);
            //client.Stand(0.1);
            //client.ActivatePopCornDispenser();
            //client.Move(80);
            //корректно завершаем работу
            client.Exit();
        }

        static void HandleSensorData(FullMapSensorData sensorData)
        {

        }
    }
}
