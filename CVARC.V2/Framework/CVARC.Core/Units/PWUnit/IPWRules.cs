using AIRLab.Mathematics;
using CVARC.Core.Units.PudgeUnit;

namespace CVARC.Core.Units.PWUnit
{
    public interface IPWRules
    {
        double HookRange{ get; set; }
        double MovementTime{ get; set; }
        double RotationTime{ get;set; }
        double TimeToHook{ get; set; }
        Angle RotationAngle{ get; set; }
        double MovementRange{ get; set; }
    }
    public interface IPWRules<TCommand>
        where TCommand : IPWCommand 
    {
    }
}
