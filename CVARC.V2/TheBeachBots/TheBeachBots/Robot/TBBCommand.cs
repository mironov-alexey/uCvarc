using System.Runtime.Serialization;

namespace CVARC.V2
{
    [DataContract]
    public class TBBCommand : ICommand, ISimpleMovementCommand, IDoorUnitCommand, 
        ISeashellGripperCommand, IFishingCommand, ISandGripperCommand, IParasolCommand
    {
        [DataMember]
        public SimpleMovement SimpleMovement { get; set; }

        [DataMember]
        public TriggerAction DoorUnitAction { get; set; }

        [DataMember]
        public SeashellGripperAction SeashellGripperAction { get; set; }

        [DataMember]
        public FishingRodAction FishingRodAction { get; set; }

        [DataMember]
        public SandGripperAction SandGripperAction { get; set; }

        [DataMember]
        public TriggerAction ParasolAction { get; set; }
    }
}
