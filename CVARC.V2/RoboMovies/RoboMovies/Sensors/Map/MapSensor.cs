using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AIRLab;
using AIRLab.Mathematics;
using CVARC.V2;

namespace RoboMovies
{
    public class MapSensor : Sensor<Map, IActor>
    {
		protected Frame3D robotLocation { get; private set; }

        public override Map Measure()
        {
			robotLocation = Actor.World.Engine.GetAbsoluteLocation(Actor.ObjectId);
            var map = new Map();
            map.Objects = Actor.World.IdGenerator.GetAllPairsOfType<RMObject>()
                .Where(z => Actor.World.Engine.ContainBody(z.Item2))
                .Select(z => Tuple.Create(z.Item1, Actor.World.Engine.GetAbsoluteLocation(z.Item2)))
                .Select(z => new RMObjectMapData(z.Item1, new PointF((float)z.Item2.X, (float)z.Item2.Y)))
                .ToArray();
            return map;
        }
    }
}
