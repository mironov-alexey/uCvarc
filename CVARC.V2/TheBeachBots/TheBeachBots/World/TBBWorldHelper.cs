using System;
using AIRLab.Mathematics;
using CVARC.V2;

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
            return location.Y < -100 && Math.Abs(location.X) > 57.2 && Math.Abs(location.X) < 100.2;
        }

        public bool IsInsideNet(Frame3D location)
        {
            return location.Y < -100 && Math.Abs(location.X) < 57.2;
        }

        double Distance(Frame3D first, Frame3D second)
        {
            return Math.Sqrt(Math.Pow(first.X - second.X, 2) + Math.Pow(first.Y - second.Y, 2));
        }

        public string ColorToControllerId(SideColor color)
        {
            if (color == SideColor.Green)
                return TwoPlayersId.Right;
            if (color == SideColor.Violet)
                return TwoPlayersId.Left;
            throw new ArgumentException("This color is not assigned to any side.");
        }

        public SideColor ControllerIdToColor(string id)
        {
            if (id == TwoPlayersId.Left)
                return SideColor.Violet;
            if (id == TwoPlayersId.Right)
                return SideColor.Green;
            throw new ArgumentException("Invalid controller id.");
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
