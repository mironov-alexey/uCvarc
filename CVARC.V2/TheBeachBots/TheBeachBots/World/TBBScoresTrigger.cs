using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVARC.V2;

namespace TheBeachBots
{
    class TBBScoresTrigger : Trigger
    {
        TBBWorld world;
        public double Interval { get; private set; }

        public TBBScoresTrigger(TBBWorld world, double interval=0.5)
        {
            this.world = world;
            Interval = ScheduledTime = interval;
        }

        public override TriggerKeep Act(double time)
        {
            world.Scores.DeleteTemporaryRecords();
            
            CheckSeashells();
            CheckSand();
            CheckFishOutsideWater();
            CheckFishInsideNet();

            ScheduledTime += Interval;
            return TriggerKeep.Keep;
        }

        private void CheckSeashells()
        {
            CheckSeashells(SideColor.Green);
            CheckSeashells(SideColor.Violet);
        }

        private void CheckSand()
        {
            CheckSand(SideColor.Green);
            CheckSand(SideColor.Violet);
        }

        private void CheckSeashells(SideColor color)
        {
            var seashells = world.IdGenerator.GetAllPairsOfType<TBBObject>()
                .Where(x => x.Item1.Type == ObjectType.Seashell)
                .Where(x => x.Item1.Color == SideColor.Any || x.Item1.Color == color)
                .Where(x => world.Engine.FindParent(x.Item2) == null)
                .Where(x => world.Helper.IsInsideBuildingArea(world.Engine.GetAbsoluteLocation(x.Item2), color));

            foreach (var seashell in seashells)
                world.Scores.Add(world.Helper.ColorToControllerId(color), 2,
                    "Valid seashell in the starting area", RecordType.Temporary);                
        }

        private void CheckFishInsideNet()
        {
            var fishInNet = world.IdGenerator.GetAllPairsOfType<TBBObject>()
                .Where(x => x.Item1.Type == ObjectType.Fish)
                .Where(x => world.Engine.FindParent(x.Item2) == null)
                .Where(x => world.Helper.IsInsideNet(world.Engine.GetAbsoluteLocation(x.Item2)))
                .Select(x => x.Item1);

            foreach (var fish in fishInNet)
                world.Scores.Add(world.Helper.ColorToControllerId(fish.Color), 5,
                    "Fish in net", RecordType.Temporary);
        }

        private void CheckFishOutsideWater()
        {
            var fishOutsideWater = world.IdGenerator.GetAllPairsOfType<TBBObject>()
                .Where(x => x.Item1.Type == ObjectType.Fish)
                .Where(x => !world.Helper.IsInsideWater(world.Engine.GetAbsoluteLocation(x.Item2)))
                .Select(x => x.Item1);

            foreach (var fish in fishOutsideWater)
                world.Scores.Add(world.Helper.ColorToControllerId(fish.Color), 5,
                    "Fish outside water", RecordType.Temporary);
        }

        private void CheckSand(SideColor color)
        {
            var sandInBuildingArea = world.IdGenerator.GetAllPairsOfType<TBBObject>()
                .Where(x => x.Item1.Type == ObjectType.SandCone || x.Item1.Type == ObjectType.SandCube
                    || x.Item1.Type == ObjectType.SandCylinder)
                .Where(x => world.Engine.FindParent(x.Item2) == null)
                .Where(x => world.Helper.IsInsideBuildingArea(world.Engine.GetAbsoluteLocation(x.Item2), color));

            foreach (var sandBox in sandInBuildingArea)
                world.Scores.Add(world.Helper.ColorToControllerId(color), 2,
                    "Sand block in building area.", RecordType.Temporary);
        }
    }
}
