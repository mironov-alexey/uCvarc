﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public abstract class RobotLocationSensor : Sensor<LocatorItem ,IActor>
    {
        bool measureSelf;

        public RobotLocationSensor(bool self)
        {
            measureSelf = self;
        }

        public override LocatorItem Measure()
        {
            var id = Actor.World.Actors
                .Where(z => (z.ControllerId == Actor.ControllerId) == measureSelf)
                .Select(z => z.ObjectId)
                .FirstOrDefault();
            if (id == null) throw new Exception("Robot is not found");
            if (!Actor.World.Engine.ContainBody(id)) throw new Exception("Robot is not in the world");
            var location = Actor.World.Engine.GetAbsoluteLocation(id);
            return new LocatorItem
            {
                Id = id,
                X = location.X,
                Y = location.Y,
                Angle = location.Yaw.Grad
            };
        }
    }

    public class SelfLocationSensor : RobotLocationSensor
    {
        public SelfLocationSensor() : base(true) { }
    }

    public class OpponentLocationSensor : RobotLocationSensor
    {
        public OpponentLocationSensor() : base(false) { }
    }
}
