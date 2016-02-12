namespace CVARC.V2
{
    public interface IParasolCommand : ICommand
    {
        TriggerAction ParasolAction { get; set; }
    }
}
