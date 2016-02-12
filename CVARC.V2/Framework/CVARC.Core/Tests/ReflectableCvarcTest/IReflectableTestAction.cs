﻿namespace CVARC.V2
{
    public interface IReflectableTestAction<TSensorData, TCommand> : ITestAction<TSensorData, TCommand>
    {
        void Reflect();
    }
}
