using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CVARC.V2
{
    public sealed class CommandBuilder<TRules,TCommand, TBack>
    {
        public CommandBuilder(TRules rules, TBack back)
        {
            Rules = rules;
            Back = back;
        }
        public readonly TRules Rules;
        public event Action<TCommand> CommandAdded;
        public readonly TBack Back;
        public void Add(TCommand command)
        {
            if (CommandAdded != null) CommandAdded(command);
        }
        public TBack To<TBack>(Func<TBack> where) { return where(); }
    }

}
