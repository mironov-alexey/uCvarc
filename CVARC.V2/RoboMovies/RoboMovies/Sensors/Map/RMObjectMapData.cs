using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace RoboMovies
{
    [DataContract]
    public struct RMObjectMapData
    {
        [DataMember]
        public readonly RMObject ObjectInfo;
        [DataMember]
        public readonly PointF Location;

        public RMObjectMapData(RMObject objInfo, PointF location)
        {
            ObjectInfo = objInfo;
            Location = location;
        }
    }
}
