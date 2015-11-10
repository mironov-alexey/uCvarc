namespace CVARC.V2
{
    public class TestAction<TSensorData, TCommand> : ITestAction<TSensorData, TCommand>
        where TCommand : ICommand
    {
        public TCommand Command { get; }
        public ISensorAsserter<TSensorData> Asserter { get; }

        public TestAction(TCommand command)
        {
            Command = command;
        }

        public TestAction(ISensorAsserter<TSensorData> asserter)
        {
            Asserter = asserter;
        }
    }
}

// FIXME: что лучше: 2 класса для команды и ассерта или один общий, но с пустыми указателями?

    //public class CommandTestAction<TSensorData, TCommand> : ITestAction<TSensorData, TCommand>
    //    where TCommand : class, ICommand
    //    where TSensorData : class
    //{
    //    public TCommand Command { get; }
    //    public ISensorAsserter<TSensorData> Asserter => null;

    //    public CommandTestAction(TCommand command)
    //    {
    //        Command = command;
    //    } 
    //}

    //public class AssertTestAction<TSensorData, TCommand> : ITestAction<TSensorData, TCommand>
    //    where TCommand : class, ICommand
    //    where TSensorData : class
    //{
    //    public TCommand Command => null;
    //    public ISensorAsserter<TSensorData> Asserter { get; }

    //    public AssertTestAction(ISensorAsserter<TSensorData> asserter)
    //    {
    //        Asserter = asserter;
    //    }
    //}
