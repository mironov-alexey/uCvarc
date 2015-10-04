namespace CVARC.V2
{
    public enum FishingRodAction
    {
        None,
        GripFish,
        ReleaseFish,
    }

    public interface IFishingCommand : ICommand
    {
        FishingRodAction FishingRodAction { get; set; }
    }
}
