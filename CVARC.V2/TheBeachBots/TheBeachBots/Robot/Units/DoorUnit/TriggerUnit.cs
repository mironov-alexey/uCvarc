using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AIRLab.Mathematics;

namespace CVARC.V2
{
    public enum TriggerAction
    {
        None, 
        Activate,
        Deactivate,
    }

    public abstract class TriggerUnit<TRules, TCommand, TWorld> : IUnit
        where TRules : IRules
        where TCommand : ICommand
        where TWorld : IWorld
    {
        protected IActor actor;
        protected TRules rules;
        protected TWorld world;

        public Frame3D ConnectionPoint { get; private set; }
        public Frame3D RobotLocation { get { return actor.World.Engine.GetAbsoluteLocation(actor.ObjectId); } }
        public Frame3D UnitLocation { get { return RobotLocation.Apply(ConnectionPoint); } }

        protected TriggerUnit(IActor actor, TWorld world, Frame3D connectionPoint)
        {
            this.actor = actor;
            this.world = world;
            this.rules = Compatibility.Check<TRules>(this, actor.Rules);
            ConnectionPoint = connectionPoint;
        }

        protected abstract string FindTrigger();
        protected abstract void ActivateTrigger(string trigger);
        protected abstract void DeactivateTrigger(string trigger);
        protected abstract double GetActivationTime(TRules rules);
        protected abstract double GetDeactivationTime(TRules rules);
        protected abstract TriggerAction GetAction(TCommand command);

        private double ProcessTrigger(Action<string> processor, double expectedResponce)
        {
            var triggerId = FindTrigger();
            if (triggerId == null || !world.Engine.ContainBody(triggerId)) return 0;
            processor(triggerId);
            return expectedResponce;
        }

        public UnitResponse ProcessCommand(object _command)
        {
            var command = Compatibility.Check<TCommand>(this, _command);

            switch (GetAction(command))
            {
                case TriggerAction.Activate:
                    return UnitResponse.Accepted(ProcessTrigger(ActivateTrigger, GetActivationTime(rules)));
                case TriggerAction.Deactivate:
                    return UnitResponse.Accepted(ProcessTrigger(DeactivateTrigger, GetDeactivationTime(rules)));
                default:
                    return UnitResponse.Denied();
            }
        }
    }
}
