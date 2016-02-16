namespace CVARC.Core.Units.PudgeUnit
{
    public enum PudgeCommand
    {
        Hook, 
        Move,
        RotateClockwise,
        RotateCounterClockwise
    }
    public interface IPWCommand
    {
        PudgeCommand Type { get; set; }
    }
}