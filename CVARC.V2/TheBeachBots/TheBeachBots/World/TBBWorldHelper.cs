using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    public class TBBWorldHelper
    {
        private readonly Frame3D buildingAreaCenter = new Frame3D(0, 25, 0);

        public bool IsInsideStartingArea(Frame3D location, SideColor color)
        {
            return Math.Abs(location.X) > 150 - 30 && location.X * Sign(color) >= 0
                && location.Y < 40 && location.Y > -10;
        }

        public bool IsInsideBuildingArea(Frame3D location, SideColor color)
        {
            return Distance(location, buildingAreaCenter) < 60
                && location.Y < 25 && location.X * Sign(color) >= 0;
        }

        public bool IsInsideWater(Frame3D location)
        {
            return location.X < -100 && Math.Abs(location.X) > 57.2 && Math.Abs(location.X) < 100.2;
        }

        public bool IsInsideNet(Frame3D location)
        {
            return location.Y < -100 && Math.Abs(location.X) < 57.2;
        }

        double Distance(Frame3D first, Frame3D second)
        {
            return Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));
        }

        int Sign(SideColor color)
        {
            switch (color)
            {
                case SideColor.Green:
                    return 1;
                case SideColor.Violet:
                    return -1;
                default:
                    return 0;
            }
        }
    }
}
