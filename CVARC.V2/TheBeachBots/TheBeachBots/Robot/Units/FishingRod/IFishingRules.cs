namespace CVARC.V2
{
    public interface IFishingRules : IRules
    {
        double FishInteractionRange { get; }
        double FishGrippingTime { get; }
        double FishReleasingTime { get; }
    }

    public interface IFishingRules<TCommand> : IFishingRules { }
}
