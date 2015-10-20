using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    public class TBBWorldHelper
    {
        private readonly Frame2D buildingAreaCenter = new Frame2D(0, 25, Angle.Zero);

        public bool IsInsideStartingArea(Frame2D location, SideColor color)
        {
            return Math.Abs(location.X) > 150 - 30 && location.X * Sign(color) > 0
                && location.Y < 40 && location.Y > -10;
        }

        public bool IsInsideBuildingArea(Frame2D location, SideColor color)
        {
            return Distance(location, buildingAreaCenter) < 60
                && location.Y < 25 && location.X * Sign(color) > 0;
        }

        public bool IsInsideWater(Frame2D location)
        {
            return location.X < -100 && Math.Abs(location.X) > 57.2 && Math.Abs(location.X) < 100.2;
        }

        public bool IsInsideNet(Frame2D location)
        {
            return location.Y < -100 && Math.Abs(location.X) < 57.2;
        }

        double Distance(Frame2D first, Frame2D second)
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
