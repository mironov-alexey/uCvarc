using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RoboMovies
{
    [DataContract]
    public class Map
    {
        [DataMember]
        public RMObjectMapData[] Objects { get; set; }
    }
}
