#define SANDCASTLES_ENABLED

using System;
using System.Linq;
using AIRLab.Mathematics;
using CVARC.V2;

namespace TheBeachBots
{
    public class TBBWorld : World<TBBWorldState, ITBBWorldManager>
    {
        public TBBWorldHelper Helper { get; private set; }

        public override void AdditionalInitialization()
        {
            Helper = new TBBWorldHelper();

            var detector = new CollisionDetector(this);
            detector.FindControllableObject = side =>
            {
                var robot = Actors
                    .OfType<TBBRobot>()
                    .Where(z => z.ObjectId == side.ObjectId /*|| z.TowerBuilder.CollectedIds.Contains(z.ObjectId)*/)
                    .FirstOrDefault();
                if (robot != null)
                {
                    side.ControlledObjectId = robot.ObjectId;
                    side.ControllerId = robot.ControllerId;
                }
            };
            detector.Account = c =>
            {
                if (!c.Victim.IsControllable) return;
                if (!detector.Guilty(c)) return;
                Scores.Add(c.Offender.ControllerId, -30, "Collision");
            };

            Scores.Add(TwoPlayersId.Left, 0, "Staring scores");
            Scores.Add(TwoPlayersId.Right, 0, "Staring scores");

            Clocks.AddTrigger(new TBBScoresTrigger(this));
        }

        public override void CreateWorld()
        {
            InitEmptyMap();
            CreateLineOf(ObjectType.BeachHut, (id, l, c) => Manager.CreateBeachHut(id, l, c),
                105, 120, 90);
            CreateLineOf(ObjectType.Fish, (id, l, c) => Manager.CreateFish(id, l, c),
                -105, 60, 70, 80, 90);

#if SANDCASTLES_ENABLED
            CreateSandCastle(new Point2D(85, 10), 1);
            CreateSandCastle(new Point2D(-85, 10), 1);
            CreateSandCastle(new Point2D(62, 92), 2);
            CreateSandCastle(new Point2D(-62, 92), 2);
            CreateSandDune();
#endif
            CreateSeashells();
        }

        void InitEmptyMap()
        {
            Manager.CreateEmptyTable();
            Manager.CreateWall(new Point2D(0, 25), new Point2D(120, 2.2));
            Manager.CreateWall(new Point2D(0, -5), new Point2D(2.2, 60));
            Manager.CreateWall(new Point2D(70, 90), new Point2D(2.2, 20));
            Manager.CreateWall(new Point2D(-70, 90), new Point2D(2.2, 20));
            Manager.CreateRock(new Point2D(150, -100));
            Manager.CreateRock(new Point2D(-150, -100));
        }

        void CreateLineOf(ObjectType type, Action<string, Point2D, SideColor> constructor,
            int locationY, params int[] locationX)
        {
            var coordinates = locationX.Union(locationX.Select(x => -x)).ToArray();

            for (var i = 0; i < coordinates.Length; ++i)
            {
                SideColor color = i < coordinates.Length / 2 ? SideColor.Green : SideColor.Violet;
                string id = IdGenerator.CreateNewId(new TBBObject(color, type));
                constructor(id, new Point2D(coordinates[i], locationY), color);
            }
        }

        void CreateSeashells()
        {
            foreach (var seashell in WorldState.Seashells)
                Manager.CreateSeashell(IdGenerator.CreateNewId(new TBBObject(seashell.Value, ObjectType.Seashell)),
                    seashell.Key, seashell.Value);
        }

        void CreateSandCastle(Point2D location, int layersCount)
        {
            int layer;
            for (layer = 0; layer < layersCount; ++layer)
                for (var i = 0; i < 4; ++i)
                    Manager.CreateSandCube(IdGenerator.CreateNewId(new TBBObject(ObjectType.SandCube)),
                        new Point3D(location.X + 3 * (i < 2 ? 1 : -1), location.Y + 3 * (i % 2 == 0 ? 1 : -1), 
                            Manager.FloorLevel + 6 * layer));
            Manager.CreateSandCylinder(IdGenerator.CreateNewId(new TBBObject(ObjectType.SandCylinder)),
                new Point3D(location.X, location.Y, Manager.FloorLevel + 6 * layer++));
            Manager.CreateSandCone(IdGenerator.CreateNewId(new TBBObject(ObjectType.SandCone)),
                new Point3D(location.X, location.Y, Manager.FloorLevel + 6 * layer++));
        }

        void CreateSandDune()
        {
            var layer = new int[] { 95, 95 - 6, 95 - 12 };
            var cubeSize = 6;
            int x;

            Action<double, double, double> addCube = (_x, _y, _z) => Manager.CreateSandCube(
                IdGenerator.CreateNewId(new TBBObject(ObjectType.SandCube)), new Point3D(_x, _y, _z));
            Action<double, double, double> addCylinder = (_x, _y, _z) => Manager.CreateSandCylinder(
                IdGenerator.CreateNewId(new TBBObject(ObjectType.SandCylinder)), new Point3D(_x, _y, _z));
            Action<double, double, double> addCone = (_x, _y, _z) => Manager.CreateSandCone(
                IdGenerator.CreateNewId(new TBBObject(ObjectType.SandCone)), new Point3D(_x, _y, _z));

            var z = Manager.FloorLevel;

            for (x = -4; x <= 4; ++x)
                addCube(cubeSize * x, layer[0], z);
            for (x = -1; x <= 1; ++x)
                addCube(cubeSize * x, layer[1], z);
            addCylinder(0, layer[2], z);

            z += cubeSize;

            for (x = -1; x <= 1; ++x)
                addCube(cubeSize * x, layer[0], z);
            addCube(0, layer[1], Manager.FloorLevel + cubeSize);
            for (x = -3; x <= 3; ++x)
                if (Math.Abs(x) > 1)
                    addCylinder(cubeSize * x, layer[0], z);
            for (x = -1; x <= 1; ++x)
                if (x != 0)
                    addCylinder(cubeSize * x, layer[1], z);

            z += cubeSize;

            for (x = -2; x <= 2; ++x)
                addCylinder(cubeSize * x, layer[0], z);
            addCylinder(0, layer[1], z);

            z += cubeSize;

            for (x = -2; x <= 2; ++x)
                if (Math.Abs(x) > 1)
                    addCone(cubeSize * x, layer[0], z);
                else
                    addCylinder(cubeSize * x, layer[0], z);

            z += cubeSize;

            for (x = -1; x <= 1; ++x)
                addCone(cubeSize * x, layer[0], z);
        }
    }
}
