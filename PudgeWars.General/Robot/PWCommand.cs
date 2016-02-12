using System.Runtime.Serialization;
using CVARC.V2;

namespace CVARC.V2
{
    [DataContract]
    public class PWCommand : ISimpleMovementCommand
    {
        [DataMember]
        public SimpleMovement SimpleMovement{ get; set; }
    }
}