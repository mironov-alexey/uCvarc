using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    [DataContract]
    public class TBBCommand : ICommand, ISimpleMovementCommand, IDoorUnitCommand, 
        ISeashellGripperCommand, IFishingCommand
    {
        [DataMember]
        public SimpleMovement SimpleMovement { get; set; }

        [DataMember]
        public DoorUnitAction DoorUnitAction { get; set; }

        [DataMember]
        public SeashellGripperAction SeashellGripperAction { get; set; }

        [DataMember]
        public FishingRodAction FishingRodAction { get; set; }
    }
}
