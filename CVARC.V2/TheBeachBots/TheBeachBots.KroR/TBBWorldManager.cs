using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AIRLab.Mathematics;
using CVARC.Core;
using CVARC.V2;


namespace TheBeachBots.KroR
{
    public static class WorldInitializerHelper
    {
        public static Stream GetResourceStream(string resourceName)
        {
            var assembly = typeof(WorldInitializerHelper).Assembly;
            var names = assembly.GetManifestResourceNames();
            return assembly.GetManifestResourceStream("TheBeachBots.KroR.Resources." + resourceName);
        }
    }

    public class TBBWorldManager : KroRWorldManager<TBBWorld>, ITBBWorldManager
    {
        static int length = 300;
        static int width = 200;
        static int halfLength = length / 2;
        static int halfWidth = width / 2;
        static int floorLevel = 3;

        public double FloorLevel { get { return floorLevel; } }

        public override void CreateWorld(IdGenerator generator) { }

        public void CreateEmptyTable()
        {
            var floorImage = new Bitmap(WorldInitializerHelper.GetResourceStream("field.png"));
            floorImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

            Root.Add(new Box
            {
                XSize = length,
                YSize = width,
                ZSize = floorLevel,
                DefaultColor = Color.White,
                IsStatic = true,
                Top = new PlaneImageBrush { Image = floorImage },
            });

            CreateBorders();
            //CreateDune();
            //CreateSeaRocks();
        }

        private void CreateBorders()
        {
            Color wallsColor = Color.CornflowerBlue;
            for (int i = 0; i < 4; ++i)
            {
                var sizeX = i / 2 == 0 ? length + 2.2 : 2.2;
                var sizeY = i / 2 == 1 ? width + 2.2 : 2.2;
                var lX = i / 2 == 0 ? width + 2.2 : 2.2;
                var lY = i / 2 == 1 ? length + 2.2 : 2.2;
                var pos = i % 2 == 0 ? 1 : -1;

                AddWall(new Box
                {
                    XSize = sizeX,
                    YSize = sizeY,
                    ZSize = 7,
                    DefaultColor = wallsColor,
                    Location = new Frame3D(pos * lY / 2, pos * lX / 2, floorLevel)
                });
            }
        }

        private Color GetDrawingColor(SideColor color)
        {
            if (color == SideColor.Any) return Color.White;
            return color == SideColor.Green ? Color.DarkGreen : Color.DarkViolet;
        }

        void AddWall(Body wall)
        {
            wall.IsStatic = true;
            wall.IsMaterial = true;
            wall.Density = Density.Wood;
            wall.NewId = "wall";
            Root.Add(wall);
        }

        public void CreateWall(Point2D location, Point2D size)
        {
            AddWall(new Box
            {
                XSize = size.X,
                YSize = size.Y,
                ZSize = 2.2,
                DefaultColor = Color.CornflowerBlue,
                Location = new Frame3D(location.X, location.Y, floorLevel),
            });
        }

        public void CreateSandCube(string id, Point3D location)
        {
            Root.Add(new Box
            {
                XSize = 5.8,
                YSize = 5.8,
                ZSize = 5.8,
                DefaultColor = Color.DarkRed,
                IsMaterial = true,
                Density = Density.PlasticPvc,
                FrictionCoefficient = 10,
                Location = new Frame3D(location.X, location.Y, location.Z),
                NewId = id,
            });
        }

        public void CreateSandCylinder(string id, Point3D location)
        {
            Root.Add(new Cylinder
            {
                RTop = 5.8 / 2,
                RBottom = 5.8 / 2,
                Height = 5.8,
                DefaultColor = Color.DarkRed,
                IsMaterial = true,
                Density = Density.PlasticPvc,
                FrictionCoefficient = 10,
                Location = new Frame3D(location.X, location.Y, location.Z),
                NewId = id,
            });
        }

        public void CreateSandCone(string id, Point3D location)
        {
            Root.Add(new Cylinder
            {
                RTop = 0,
                RBottom = 5.8 / 2,
                Height = 6,
                DefaultColor = Color.DarkRed,
                IsMaterial = true,
                Density = Density.PlasticPvc,
                FrictionCoefficient = 10,
                Location = new Frame3D(location.X, location.Y, location.Z),
                NewId = id,
            });
        }

        public void CreateSeashell(string id, Point2D location, SideColor color)
        {
            Root.Add(new Cylinder
            {
                RTop = 7.62 / 2,
                RBottom = 7.62 / 2,
                Height = 2.5,
                DefaultColor = GetDrawingColor(color),
                IsMaterial = true,
                Density = Density.PlasticPvc,
                FrictionCoefficient = 10,
                Location = new Frame3D(location.X, location.Y, floorLevel),
                NewId = id,
            });
        }

        public void CreateFish(string id, Point2D location, SideColor color)
        {
            Root.Add(new Ball
            {
                Radius = 5,
                Location = new Frame3D(location.X, location.Y, floorLevel * 2),
                DefaultColor = GetDrawingColor(color),
                NewId = id,
                IsMaterial = true,
                Density = Density.PlasticPvc,
                FrictionCoefficient = 10,
            });
        }

        public void CreateBeachHut(string id, Point2D location, SideColor color)
        {
            Root.Add(new Box
            {
                XSize = 12,
                YSize = 12,
                ZSize = 16,
                DefaultColor = GetDrawingColor(color),
                IsStatic = true,
                Location = new Frame3D(location.X, location.Y, floorLevel),
                NewId = id,
            });
        }

        public void CloseBeachHut(string id)
        {
            Root.Remove(Engine.GetBody(id));
        }

        public void CreateRock(Point2D location)
        {
            AddWall(new Cylinder
            {
                RTop = 25,
                RBottom = 25,
                Height = 4.4,
                IsMaterial = true,
                IsStatic = true,
                DefaultColor = Color.Gray,
                Location = new Frame3D(location.X, location.Y, floorLevel),                
            });
        }
    }
}
