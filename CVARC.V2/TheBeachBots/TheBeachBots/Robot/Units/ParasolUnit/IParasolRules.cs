namespace CVARC.V2
{
    public interface IParasolRules : IRules 
    {
        double ParasolOpeningTime { get; }
        double ParasolClosingTime { get; }
    }
    
    public interface IParasolRules<TCommand> : IParasolRules { }
}
