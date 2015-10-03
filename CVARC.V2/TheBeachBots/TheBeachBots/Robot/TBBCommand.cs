using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    [DataContract]
    public class TBBCommand : ICommand, ISimpleMovementCommand
    {
        [DataMember]
        public SimpleMovement SimpleMovement { get; set; }
    }
}
