using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBeachBots;
using AIRLab.Mathematics;

namespace CVARC.V2
{
    public class DoorUnit : TriggerUnit<IDoorOpeningRules, IDoorUnitCommand, TBBWorld>     
    {
        public DoorUnit(IActor actor, TBBWorld world, Frame3D connectionPoint)
            : base(actor, world, connectionPoint) { }

        private double Distance(Frame3D first, Frame3D second)
        {
            var delta = first.Invert().Apply(second);
            return Math.Sqrt(delta.X * delta.X + delta.Y * delta.Y + delta.Z * delta.Z);
        }

        protected override string FindTrigger()
        {
            return actor.World.IdGenerator.GetAllPairsOfType<TBBObject>()
                .Where(x => x.Item1.Type == ObjectType.BeachHut)
                .Where(x => actor.World.Engine.ContainBody(x.Item2))
                .Where(x => Distance(UnitLocation, world.Engine.GetAbsoluteLocation(x.Item2)) < rules.DoorInteractionRange)
                .Select(x => x.Item2)
                .FirstOrDefault();
        }

        protected override bool ActivateTrigger(string trigger)
        {
            world.Manager.CloseBeachHut(trigger);
            return true;
        }

        protected override bool DeactivateTrigger(string trigger)
        {
            world.Manager.OpenBeachHut(trigger);
            return true;
        }

        protected override double GetActivationTime(IDoorOpeningRules rules)
        {
            return rules.DoorOpeningTime;
        }

        protected override double GetDeactivationTime(IDoorOpeningRules rules)
        {
            return rules.DoorClosingTime;
        }

        protected override TriggerAction ExtractAction(IDoorUnitCommand command)
        {
            return command.DoorUnitAction;
        }
    }
}
