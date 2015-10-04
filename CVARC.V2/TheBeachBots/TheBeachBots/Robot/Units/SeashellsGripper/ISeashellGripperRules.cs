using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public interface ISeashellGripperRules : IRules 
    {
        double SeashellInteractionRange { get; }
        double SeashellGrippingTime { get; }
        double SeashellReleasingTime { get; }
    }

    public interface ISeashellGripperRules<TCommand> : ISeashellGripperRules { }
}
