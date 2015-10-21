using System;
using System.Collections.Generic;
using System.Linq;
using TheBeachBots;
using AIRLab.Mathematics;

namespace CVARC.V2
{
    public class SandGripper : BaseGripperUnit<ISandGripperRules>
    {
        TBBWorld world;

        public double Range { get; set; }
        public Stack<string> CollectedDetails { get; private set; }

        public Action<string> OnGrip;
        public Action<string, Frame3D> OnRelease;

        public SandGripper(IActor actor, TBBWorld world, Frame3D grippingPoint, double range)
            : base(actor)
        {
            this.world = world;
            CollectedDetails = new Stack<string>();
            GrippingPoint = grippingPoint;
            Range = range;
        }

        protected string FindSandCastleDetail()
        {
            return world.IdGenerator.GetAllPairsOfType<TBBObject>()
                .Where(x => x.Item1.Type == ObjectType.SandCone ||
                        x.Item1.Type == ObjectType.SandCube || x.Item1.Type == ObjectType.SandCylinder)
                .Where(x => actor.World.Engine.ContainBody(x.Item2))
                .Where(x => GetAvailability(x.Item2).Distance < Range)
                .Select(x => x.Item2)
                .FirstOrDefault();
        }

        void Collect()
        {
            if (CollectedDetails.Count >= rules.SandGripperCapacity) return;

            var objectToCollect = FindSandCastleDetail();
            if (objectToCollect == null) return;

            actor.World.Engine.Attach(objectToCollect, actor.ObjectId, GrippingPoint);

            if (OnGrip != null)
                OnGrip(objectToCollect);

            CollectedDetails.Push(objectToCollect);
        }

        void Release()
        {
            if (CollectedDetails.Count <= 0) return;

            var releasingObject = CollectedDetails.Pop();
            var dropLocation = actor.World.Engine.GetAbsoluteLocation(releasingObject);

            if (OnRelease == null)
                actor.World.Engine.Detach(releasingObject, dropLocation);
            else
                OnRelease(releasingObject, dropLocation);
        }

        public override UnitResponse ProcessCommand(object _command)
        {
            var command = Compatibility.Check<ISandGripperCommand>(this, _command);

            switch (command.SandGripperAction)
            {
                case SandGripperAction.CollectDetail:
                    Collect();
                    return UnitResponse.Accepted(rules.SandCollectingTime);
                case SandGripperAction.ReleaseDetail:
                    Release();
                    return UnitResponse.Accepted(rules.SandReleasingTime);
                default:
                    return UnitResponse.Denied();
            }
        }
    }
}
