using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AIRLab.Mathematics;
using CVARC.V2;

namespace TheBeachBots
{
    [DataContract]
    public class TBBWorldState : IWorldState
    {
        [DataMember]
        public Dictionary<Point2D, SideColor> Seashells { get; private set; }

        public TBBWorldState(Dictionary<Point2D, SideColor> seashells) { Seashells = seashells; }

        public TBBWorldState(IEnumerable<Seashell> seashells)
            : this(seashells.ToDictionary(x => x.Position, x => x.Color)) { }

        public TBBWorldState(ushort seed)
            : this(TBBWorldSettings.GenerateSeashellsLayout(seed)) { }
    }
}
