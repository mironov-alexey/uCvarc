using System;
using System.Linq;
using TheBeachBots;
using AIRLab.Mathematics;

namespace CVARC.V2
{
    public class SeashellGripper : BaseTBBGripper<ISeashellGripperRules, ISeashellGripperCommand, TBBWorld>
    {
        public SeashellGripper(IActor actor, TBBWorld world, Frame3D grippingPoint)
            : base(actor, world, grippingPoint) { }
        
        protected override string FindDetail()
        {
            return World.IdGenerator.GetAllPairsOfType<TBBObject>()
                .Where(x => x.Item1.Type == ObjectType.Seashell)
                .Where(x => actor.World.Engine.ContainBody(x.Item2))
                .Where(x => GetAvailability(x.Item2).Distance < rules.SeashellInteractionRange)
                .Select(x => x.Item2)
                .FirstOrDefault();
        }

        protected override GripperAction GetGripperAction(ISeashellGripperCommand command)
        {
            switch(command.SeashellGripperAction)
            {
                case SeashellGripperAction.Grip:
                    return GripperAction.Grip;
                case SeashellGripperAction.Release:
                    return GripperAction.Release;
                case SeashellGripperAction.No:
                    return GripperAction.No;
            }
            throw new Exception("Cannot reach this part of code.");
        }

        protected override BaseTBBGripperRules ConvertRules(ISeashellGripperRules rules)
        {
            return new BaseTBBGripperRules(rules.SeashellGrippingTime, rules.SeashellReleasingTime);
        }
    }
}
