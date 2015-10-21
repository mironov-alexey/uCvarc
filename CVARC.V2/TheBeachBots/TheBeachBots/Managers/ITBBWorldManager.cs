using CVARC.V2;
using AIRLab.Mathematics;

namespace TheBeachBots
{
    public interface ITBBWorldManager : IWorldManager
    {
        double FloorLevel { get; }

        void CreateEmptyTable();
        void CreateRock(Point2D location);
        void CreateWall(Point2D location, Point2D size);

        void CreateSandCube(string id, Point3D location);
        void CreateSandCylinder(string id, Point3D location);
        void CreateSandCone(string id, Point3D location);

        void CreateSeashell(string id, Point2D location, SideColor color);
        void CreateFish(string id, Point2D location, SideColor color);
        void CreateBeachHut(string id, Point2D location, SideColor color);

        void CloseBeachHut(string id);
        void OpenBeachHut(string id);

        void OpenParasol(string actorId);
        void CloseParasol(string actorId);
    }
}
