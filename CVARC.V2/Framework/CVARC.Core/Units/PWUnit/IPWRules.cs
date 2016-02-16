using CVARC.Core.Units.PudgeUnit;

namespace CVARC.Core.Units.PWUnit
{
    public interface IPWRules
    {
        double HookRange{ get; set; }
        double TimeToMove{ get; set; }
        double TimeToRotate{ get;set; }
        double TimeToHook{ get; set; }
    }
    public interface IPWRules<TCommand>
        where TCommand : IPWCommand 
    {
    }
}
