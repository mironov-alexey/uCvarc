namespace CVARC.V2
{
    public interface IDoorUnitCommand : ICommand
    {
        TriggerAction DoorUnitAction { get; set; }
    }
}
