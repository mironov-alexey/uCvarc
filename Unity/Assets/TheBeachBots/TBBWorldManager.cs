using System;
using CVARC.V2;
using UnityEngine;
using TheBeachBots;
using AIRLab.Mathematics;

namespace Assets
{
    public class TBBWorldManager : ITBBWorldManager
    {
        /* 
         * TODO: 
         * 
         * + 1. fix flying walls in dune
         * + 2. tweak robot units to not interact with gripped items
         * + 3. fix seashells layout at field edges
         * + 4. fix scoring and door closing
         * 5. create fish net and water
         * + 6. moar tests
         * + 7a. create models for cones
         * 7b. create models for rocks and beach huts
         *
         */

        TBBWorld world;

        static int length = 300;
        static int width = 200;
        static int halfLength = length / 2;
        static int halfWidth = width / 2;
        static int floorLevel = 3;

        public double FloorLevel { get { return floorLevel; } }
        public void CreateWorld(IdGenerator generator) { }

        GameObject conePrefab = Resources.Load<GameObject>("Cone");

        public void Initialize(IWorld world)
        {
            this.world = (TBBWorld)world;
        }

        public void CreateEmptyTable()
        {
            var floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
            floor.transform.position = Vector3.zero;
            floor.transform.rotation = Quaternion.Euler(0, 180, 0);
            floor.transform.localScale = new Vector3(30, 1, 20);
            floor.GetComponent<Renderer>().material.mainTexture = Resources.Load<Texture2D>("TBBFloorTexture");
            floor.name = "floor";

            var net = GameObject.CreatePrimitive(PrimitiveType.Plane);
            net.transform.position = new Vector3(0, 1, -125);
            net.transform.rotation = Quaternion.Euler(0, 180, 0);
            net.transform.localScale = new Vector3(/*11.4f*/ 30, 1, 5);
            net.GetComponent<Renderer>().material.color = Color.gray;
            net.name = "net";

            var light = new GameObject("sunshine");
            light.AddComponent<Light>();
            light.GetComponent<Light>().type = LightType.Point;
            light.GetComponent<Light>().range = 1000;
            light.GetComponent<Light>().intensity = 2;
            light.transform.position = new Vector3(0, 200, 0);

            GameObject.Destroy(GameObject.Find("Point light"));

            CreateBorders();
        }

        private void CreateBorders()
        {
            var length = 300;
            var width = 200;

            for (int i = 0; i < 4; ++i)
            {
                var offset = 2.2f;

                var sizeX = i / 2 == 0 ? length + offset : offset;
                var sizeZ = i / 2 == 1 ? width + offset : offset;
                var posX = i / 2 == 1 ? length + offset : offset;
                var posZ = i / 2 == 0 ? width + offset : offset;
                var side = i % 2 == 0 ? 1 : -1;

                var border = GameObject.CreatePrimitive(PrimitiveType.Cube);
                border.transform.position = new Vector3(side * posX / 2, 5, side * posZ / 2);
                border.transform.localScale = new Vector3(sizeX, 10, sizeZ);
                border.GetComponent<Renderer>().material.color = Color.blue;
            }
        }

        private Color GetDrawingColor(SideColor color)
        {
            if (color == SideColor.Any) return Color.white;
            return color == SideColor.Green ? Color.green : Color.yellow;
        }

        public void CreateWall(Point2D location, Point2D size)
        {
            var wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            float sizeZ = floorLevel;
            wall.transform.position = new Vector3((float)location.X, sizeZ / 2, (float)location.Y);
            wall.transform.localScale = new Vector3((float)size.X, sizeZ, (float)size.Y);
            wall.GetComponent<Renderer>().material.color = Color.white;
        }

        public void CreateSandCube(string id, Point3D location)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(5.8f, 5.8f, 5.8f);

            InitSandObject(cube, id, location);
        }

        public void CreateSandCylinder(string id, Point3D location)
        {
            var cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.localScale = new Vector3(5.8f, 5.8f / 2, 5.8f);

            InitSandObject(cylinder, id, location);
        }

        public void CreateSandCone(string id, Point3D location)
        {
            var cone = GameObject.Instantiate(conePrefab);
            cone.transform.localScale = new Vector3(5.8f / 2, 5.8f / 2, 5.8f / 2);

            InitSandObject(cone, id, location);
        }

        public void InitSandObject(GameObject obj, string id, Point3D location)
        {
            obj.name = id;

            obj.transform.position = new Vector3((float)location.X, (float)location.Z, (float)location.Y);

            obj.GetComponent<Renderer>().material.color = Color.yellow;

            obj.AddComponent<Rigidbody>();
            obj.GetComponent<Rigidbody>().drag = obj.GetComponent<Rigidbody>().angularDrag = 4;
            obj.GetComponent<Rigidbody>().useGravity = true;
            obj.GetComponent<Rigidbody>().mass = 0.3f;
            obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        public void CreateSeashell(string id, Point2D location, SideColor color)
        {
            var seashell = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            var locationZ = Math.Abs(location.X) > 120 && location.Y < -70 ? floorLevel + 6 : floorLevel;
            seashell.transform.position = new Vector3((float)location.X, locationZ, (float)location.Y);
            seashell.transform.localScale = new Vector3(7.62f, 2.5f, 7.62f);

            seashell.GetComponent<Renderer>().material.color = GetDrawingColor(color);

            seashell.GetComponent<CapsuleCollider>().enabled = false;
            seashell.AddComponent<MeshCollider>();
            seashell.GetComponent<MeshCollider>().convex = true;

            seashell.AddComponent<Rigidbody>();
            seashell.GetComponent<Rigidbody>().drag = seashell.GetComponent<Rigidbody>().angularDrag = 4;
            seashell.GetComponent<Rigidbody>().useGravity = true;
            seashell.GetComponent<Rigidbody>().mass = 0.3f;
            
            seashell.name = id;
        }

        public void CreateRock(Point2D location)
        {
            var rock = GameObject.CreatePrimitive(PrimitiveType.Cylinder);

            rock.transform.position = new Vector3((float)location.X, floorLevel, (float)location.Y);
            rock.transform.localScale = new Vector3(60, 4.4f, 60);
            rock.GetComponent<CapsuleCollider>().enabled = false;
            rock.AddComponent<MeshCollider>();
        }

        public void CreateFish(string id, Point2D location, SideColor color)
        {
            var fish = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            fish.transform.position = new Vector3((float)location.X, floorLevel + 5, (float)location.Y);
            fish.transform.localScale = new Vector3(5.3f, 5.3f, 9.7f);

            fish.GetComponent<Renderer>().material.color = GetDrawingColor(color);

            fish.AddComponent<Rigidbody>();
            fish.GetComponent<Rigidbody>().drag = fish.GetComponent<Rigidbody>().angularDrag = 4;
            fish.GetComponent<Rigidbody>().useGravity = true;
            fish.GetComponent<Rigidbody>().mass = 0.3f;
            fish.name = id;
        }

        public void CreateBeachHut(string id, Point2D location, SideColor color)
        {
            var hut = GameObject.CreatePrimitive(PrimitiveType.Cube);

            Point3D size = new Point3D(12, 12, 16);

            hut.transform.position = new Vector3((float)location.X, (float)size.Z / 2, (float)location.Y);
            hut.transform.localScale = new Vector3((float)size.X, (float)size.Z, (float)size.Y);

            hut.GetComponent<Renderer>().material.color = GetDrawingColor(color);
            hut.name = id;
        }

        public void CloseBeachHut(string id)
        {
            GameObject.Destroy(GameObject.Find(id));
        }

        public void OpenBeachHut(string id)
        {
        }

        public void OpenParasol(string actorId)
        {
            var parasolAnimator = GameObject.Find(actorId + "Parasol").GetComponent<Animator>();
            parasolAnimator.Play("Open");
        }

        public void CloseParasol(string actorId)
        {
            var parasolAnimator = GameObject.Find(actorId + "Parasol").GetComponent<Animator>();
            parasolAnimator.Play("Close");
        }
    }
}