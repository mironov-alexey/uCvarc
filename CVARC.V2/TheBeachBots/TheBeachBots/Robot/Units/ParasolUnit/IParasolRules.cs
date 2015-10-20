using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public interface IParasolRules : IRules 
    {
        double ParasolOpeningTime { get; }
        double ParasolClosingTime { get; }
    }
    
    public interface IParasolRules<TCommand> : IParasolRules { }
}
