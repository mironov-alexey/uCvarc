using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoboMovies
{
    public class TestingClient : RMClient<RMSensorData>
    {
        public override string LevelName
        {
            get { return "Level1"; }
        }
    }
}
