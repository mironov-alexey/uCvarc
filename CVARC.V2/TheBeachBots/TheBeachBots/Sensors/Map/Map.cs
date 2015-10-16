using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TheBeachBots
{
    [DataContract]
    public class Map<TObj>
    {
        [DataMember]
        public GameObjectMapData<TObj>[] Objects { get; set; }
    }
}
