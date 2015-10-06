using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TheBeachBots
{
    [DataContract]
    public class Map<Tobj>
    {
        [DataMember]
        public GameObjectMapData<Tobj>[] Objects { get; set; }
    }
}
