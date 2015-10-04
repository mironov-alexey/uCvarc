using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBeachBots;
using AIRLab.Mathematics;

namespace CVARC.V2
{
    public class FishingRod : BaseTBBGripper<IFishingRules, IFishingCommand, TBBWorld>
    {
        public double Range { get; set; }

        public FishingRod(IActor actor, TBBWorld world, Frame3D grippingPoint, double range)
            : base(actor, world, grippingPoint) 
        {
            Range = range;
        }

        protected override string FindDetail()
        {
            return World.IdGenerator.GetAllPairsOfType<TBBObject>()
                .Where(x => x.Item1.Type == ObjectType.Fish)
                .Where(x => actor.World.Engine.ContainBody(x.Item2))
                .Where(x => GetAvailability(x.Item2).Distance < Range)
                .Select(x => x.Item2)
                .FirstOrDefault();
        }

        protected override GripperAction GetGripperAction(IFishingCommand command)
        {
            switch(command.FishingRodAction)
            {
                case FishingRodAction.GripFish:
                    return GripperAction.Grip;
                case FishingRodAction.ReleaseFish:
                    return GripperAction.Release;
                case FishingRodAction.None:
                    return GripperAction.No;
            }
            throw new Exception("Cannot reach this part of code.");
        }

        protected override BaseTBBGripperRules ConvertRules(IFishingRules rules)
        {
            return new BaseTBBGripperRules(rules.FishGrippingTime, rules.FishReleasingTime);
        }
    }
}
