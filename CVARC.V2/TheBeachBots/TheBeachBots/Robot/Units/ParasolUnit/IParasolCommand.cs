using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public interface IParasolCommand : ICommand
    {
        TriggerAction ParasolAction { get; set; }
    }
}
