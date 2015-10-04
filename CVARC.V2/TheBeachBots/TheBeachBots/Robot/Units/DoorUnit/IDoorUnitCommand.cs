using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public enum DoorUnitAction
    {
        None,
        CloseDoor,
        OpenDoor,
    }

    public interface IDoorUnitCommand : ICommand
    {
        DoorUnitAction DoorUnitAction { get; set; }
    }
}
