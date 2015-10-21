namespace CVARC.V2
{
    public enum SeashellGripperAction
    {
        No,
        Grip,
        Release,
    }

    public interface ISeashellGripperCommand : ICommand
    {
        SeashellGripperAction SeashellGripperAction { get; set; }
    }
}
