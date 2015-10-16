using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TheBeachBots
{
    [DataContract]
    public struct GameObjectMapData<TObj>
    {
        [DataMember]
        public readonly TObj ObjectInfo;
        [DataMember]
        public readonly PointF Location;

        public GameObjectMapData(TObj objInfo, PointF location)
        {
            ObjectInfo = objInfo;
            Location = location;
        }
    }
}
