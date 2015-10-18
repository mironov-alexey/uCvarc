using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public interface ISandGripperRules : IRules
    {
        double SandCollectingTime { get; }
        double SandReleasingTime { get; }
        int SandGripperCapacity { get; }
    }

    public interface ISandGripperRules<TCommand> : ISandGripperRules { }
}
