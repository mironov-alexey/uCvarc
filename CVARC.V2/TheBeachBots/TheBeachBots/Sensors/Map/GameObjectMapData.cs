using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TheBeachBots
{
    [DataContract]
    public struct GameObjectMapData<Tobj>
    {
        [DataMember]
        public readonly Tobj ObjectInfo;
        [DataMember]
        public readonly PointF Location;

        public GameObjectMapData(Tobj objInfo, PointF location)
        {
            ObjectInfo = objInfo;
            Location = location;
        }
    }
}
