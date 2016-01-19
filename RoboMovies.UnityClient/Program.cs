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
            client.Configurate(14001, false, RoboMoviesBots.Stand, ip: "127.0.0.1", cvarcTag: "6808f2b8-e626-4d78-9ccb-fd2670689f96");
            Control(client);
        }

        static void Control(RMClient<FullMapSensorData> client)
        {
            // управляем роботом
            client.Move(65);
            client.Rotate(-90);
            // коллект пока не работает((
            client.Collect();
            client.Rotate(-90);
            client.Move(70);
            
            //корректно завершаем работу
            client.Exit();
        }

        static void HandleSensorData(FullMapSensorData sensorData)
        {
            
        }
    }
}
