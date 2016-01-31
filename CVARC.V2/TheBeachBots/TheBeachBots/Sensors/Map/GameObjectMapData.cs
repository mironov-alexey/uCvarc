using System.Drawing;
using System.Runtime.Serialization;

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
