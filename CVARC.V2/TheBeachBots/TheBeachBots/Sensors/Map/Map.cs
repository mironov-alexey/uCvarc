using System.Runtime.Serialization;

namespace TheBeachBots
{
    [DataContract]
    public class Map<TObj>
    {
        [DataMember]
        public GameObjectMapData<TObj>[] Objects { get; set; }
    }
}
