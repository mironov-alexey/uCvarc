namespace CVARC.V2
{
    public enum SandGripperAction
    {
        None,
        CollectDetail,
        ReleaseDetail,
    }

    public interface ISandGripperCommand : ICommand
    {
        SandGripperAction SandGripperAction { get; set; }
    }
}
