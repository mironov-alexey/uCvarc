using CVARC.V2;

namespace Assets
{
    public class TBBManagerPart : ManagerPart
    {
        public TBBManagerPart() : base(() => new TBBWorldManager()) { }

        public override IActorManager CreateActorManagerFor(IActor actor)
        {
            return new TBBActorManager();
        }
    }
}
