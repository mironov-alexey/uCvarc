namespace CVARC.V2
{
    public interface ISandGripperRules : IRules
    {
        double SandCollectingTime { get; }
        double SandReleasingTime { get; }
        int SandGripperCapacity { get; }
    }

    public interface ISandGripperRules<TCommand> : ISandGripperRules { }
}
