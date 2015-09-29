using CVARC.V2;
using System;

namespace Assets.Temp
{
    public interface IRunner : IDisposable
    {
        IWorld CreateWorld();
        bool CanStart { get; }
        bool CanInterrupt { get; }
    }
}
