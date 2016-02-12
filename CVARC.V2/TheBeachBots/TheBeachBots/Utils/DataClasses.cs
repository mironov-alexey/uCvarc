namespace TheBeachBots
{
    public enum SideColor
    {
        Any,
        Green,
        Violet,
    }

    public enum ObjectType
    {
        SandCube = 0, 
        SandCylinder,
        SandCone,       // FIXME: too much sand
        
        Seashell,
        BeachHut,
        Fish,
    }

    public struct TBBObject
    {
        public readonly SideColor Color;
        public readonly ObjectType Type;

        public TBBObject(SideColor color, ObjectType type)
        {
            Color = color;
            Type = type;
        }

        public TBBObject(ObjectType type) : this(SideColor.Any, type) { }

        public override bool Equals(object obj)
        {
            if (!(obj is TBBObject)) return false;
            var tbbObject = (TBBObject)obj;
            return tbbObject.GetHashCode() == this.GetHashCode() && 
                tbbObject.Color == this.Color && tbbObject.Type == this.Type;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (int)Color * 1023 ^ (int)Type;
            }
        }
    }
}
