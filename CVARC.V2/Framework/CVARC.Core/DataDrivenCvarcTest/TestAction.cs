namespace CVARC.V2
{
    public delegate void Asserter<TSensorData>(TSensorData data, IAsserter asserter);

    public class TestAction<TSensorData, TCommand>
    {
        public TCommand Command;
        public Asserter<TSensorData> Asserter;
    }
}
