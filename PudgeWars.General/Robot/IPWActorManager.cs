using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIRLab.Mathematics;
using CVARC.V2;

namespace PudgeWars.General.Robot
{
    public interface IPWActorManager : IActorManager
    {
        void RemoveObject(string objectId);
        void CreateEmptyTable();
        void CreateLight(string lightId, Point2D location);
        void CreateStand(string standId, Point2D location, SideColor color);
        void CreateClapperboard(string clapperboardId, Point2D location, SideColor color);
        void CreatePopCorn(string popcornId, Point2D location);
        void CreatePopCornDispenser(string dispenserId, Point2D location);
        void ClimbUpStairs(string actorId, string stairsId);
    }
}
