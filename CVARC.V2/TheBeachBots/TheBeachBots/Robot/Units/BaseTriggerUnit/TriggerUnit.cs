using System;
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

        public Action<string> OnActivation { get; set; }
        public Action<string> OnDeactivation { get; set; }

        protected TriggerUnit(IActor actor, TWorld world, Frame3D connectionPoint)
        {
            this.actor = actor;
            this.world = world;
            this.rules = Compatibility.Check<TRules>(this, actor.Rules);
            ConnectionPoint = connectionPoint;
        }

        protected abstract string FindTrigger();
        protected abstract bool ActivateTrigger(string trigger);
        protected abstract bool DeactivateTrigger(string trigger);
        protected abstract double GetActivationTime(TRules rules);
        protected abstract double GetDeactivationTime(TRules rules);
        protected abstract TriggerAction ExtractAction(TCommand command);

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

            switch (ExtractAction(command))
            {
                case TriggerAction.Activate:
                    return UnitResponse.Accepted(ProcessTrigger(x =>
                    {                        
                        if (ActivateTrigger(x) && OnActivation != null )
                            OnActivation(x);
                    }, 
                    GetActivationTime(rules)));
                case TriggerAction.Deactivate:
                    return UnitResponse.Accepted(ProcessTrigger(x =>
                    {
                        if (DeactivateTrigger(x) && OnDeactivation != null)
                            OnDeactivation(x); 
                    }, 
                    GetDeactivationTime(rules)));
                default:
                    return UnitResponse.Denied();
            }
        }
    }
}
