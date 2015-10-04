using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public interface IDoorOpeningRules : IRules 
    {
        double DoorInteractionRange { get; }
        double DoorOpeningTime { get; }
        double DoorClosingTime { get; }
    }
    
    public interface IDoorOpeningRules<TCommand> : IDoorOpeningRules { }
}
