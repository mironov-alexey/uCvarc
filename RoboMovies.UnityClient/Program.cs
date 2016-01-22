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
            client.Configurate(14001, false, RoboMoviesBots.Stand, ip: "127.0.0.1", cvarcTag: "00000000-0000-0000-0000-000000000000");
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
