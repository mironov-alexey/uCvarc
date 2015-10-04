using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBeachBots;
using AIRLab.Mathematics;

namespace CVARC.V2
{
    public abstract class BaseTBBGripper<TRules, TCommand, TWorld> : BaseGripperUnit<TRules>     
    {
        public TWorld World { get; private set; }
        public BaseTBBGripperRules Rules { get; private set; }
        public string GrippedObjectId { get; private set; }

        public Action<string> OnGrip;
        public Action<string, Frame3D> OnRelease;

        public BaseTBBGripper(IActor actor, TWorld world, Frame3D grippingPoint)
            : base(actor) 
        {
            World = world;
            GrippingPoint = grippingPoint;
            Rules = ConvertRules(rules);
        }

        protected abstract string FindDetail();
        protected abstract GripperAction GetGripperAction(TCommand command);
        protected abstract BaseTBBGripperRules ConvertRules(TRules rules);

        void Grip()
        {
            if (GrippedObjectId != null) return;
            
            GrippedObjectId = FindDetail();
            if (GrippedObjectId == null) return;
            
            actor.World.Engine.Attach(GrippedObjectId, actor.ObjectId, GrippingPoint);

            if (OnGrip != null)
                OnGrip(GrippedObjectId);
        }

        void Release()
        {
            if (GrippedObjectId == null) return;
            var dropLocation = actor.World.Engine.GetAbsoluteLocation(GrippedObjectId);
            
            if (OnRelease == null)
                actor.World.Engine.Detach(GrippedObjectId, dropLocation);
            else
                OnRelease(GrippedObjectId, dropLocation);
            
            GrippedObjectId = null;
        }

        public override UnitResponse ProcessCommand(object _command)
        {
            var command = Compatibility.Check<TCommand>(this, _command);

            switch(GetGripperAction(command))
            {
                case GripperAction.Grip:
                    Grip();
                    return UnitResponse.Accepted(Rules.GrippingTime);
                case GripperAction.Release:
                    Release();
                    return UnitResponse.Accepted(Rules.ReleasingTime);
                default:
                    return UnitResponse.Denied();
            }
        }
    }
}
