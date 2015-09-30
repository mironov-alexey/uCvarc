using CVARC.V2;
using System;


namespace Assets
{
    public interface IRunner : IDisposable
    {
        IWorld CreateWorld();
        IWorld World { get; }
        string Name { get; }
        bool CanStart { get; }
        bool CanInterrupt { get; }
    }
}
