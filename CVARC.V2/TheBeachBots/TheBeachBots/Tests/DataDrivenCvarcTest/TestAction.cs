namespace CVARC.V2
{
    public delegate void Asserter<TSensorData, TWorld>(TSensorData data, TWorld world, IAsserter asserter);

    public class TestAction<TSensorData, TCommand, TWorld>
    {
        public TCommand Command;
        public Asserter<TSensorData, TWorld> Asserter;
    }
}
