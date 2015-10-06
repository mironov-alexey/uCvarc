using System;
using System.Drawing;
using System.Linq;
using System.Text;
using AIRLab.Mathematics;
using CVARC.V2;

namespace TheBeachBots
{
    public class MapSensor<Tobj> : Sensor<Map<Tobj>, IActor>
    {
		protected Frame3D robotLocation { get; private set; }

        public override Map<Tobj> Measure()
        {
			robotLocation = Actor.World.Engine.GetAbsoluteLocation(Actor.ObjectId);
            var map = new Map<Tobj>();
            map.Objects = Actor.World.IdGenerator.GetAllPairsOfType<Tobj>()
                .Where(z => Actor.World.Engine.ContainBody(z.Item2))
                .Select(z => Tuple.Create(z.Item1, Actor.World.Engine.GetAbsoluteLocation(z.Item2)))
                .Select(z => new GameObjectMapData<Tobj>(z.Item1, new PointF((float)z.Item2.X, (float)z.Item2.Y)))
                .ToArray();
            return map;
        }
    }
}
