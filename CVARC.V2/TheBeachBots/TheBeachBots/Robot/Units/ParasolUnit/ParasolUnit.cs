using TheBeachBots;
using AIRLab.Mathematics;

namespace CVARC.V2
{
    public class ParasolUnit : TriggerUnit<IParasolRules, IParasolCommand, TBBWorld>     
    {
        public ParasolUnit(IActor actor, TBBWorld world)
            : base(actor, world, Frame3D.Identity) { }

        public bool IsActivated { get; private set; }

        protected override string FindTrigger()
        {
            return actor.ObjectId;
        }

        protected override bool ActivateTrigger(string trigger)
        {
            world.Manager.OpenParasol(actor.ObjectId);
            IsActivated = true;
            return true;
        }

        protected override bool DeactivateTrigger(string trigger)
        {
            world.Manager.CloseParasol(actor.ObjectId);
            IsActivated = false;
            return true;
        }

        protected override double GetActivationTime(IParasolRules rules)
        {
            return rules.ParasolOpeningTime;
        }

        protected override double GetDeactivationTime(IParasolRules rules)
        {
            return rules.ParasolClosingTime;
        }

        protected override TriggerAction ExtractAction(IParasolCommand command)
        {
            return command.ParasolAction;
        }
    }
}
