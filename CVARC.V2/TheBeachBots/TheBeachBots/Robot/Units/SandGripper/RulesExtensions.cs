using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CVARC.V2
{
	public static partial class RulesExtensions
	{
		public static void AddSandGripperKeys<TCommand>(this ISandGripperRules<TCommand> rules, 
            KeyboardController<TCommand> pool, string controllerId)
			where TCommand : ISandGripperCommand, new()
		{
			if (controllerId == TwoPlayersId.Left)
			{
                pool.Add(Keys.C, () => new TCommand { SandGripperAction = SandGripperAction.CollectDetail });
                pool.Add(Keys.V, () => new TCommand { SandGripperAction = SandGripperAction.ReleaseDetail });
			}
			if (controllerId == TwoPlayersId.Right)
			{
                pool.Add(Keys.Oemcomma, () => new TCommand { SandGripperAction = SandGripperAction.CollectDetail });
                pool.Add(Keys.OemPeriod, () => new TCommand { SandGripperAction = SandGripperAction.ReleaseDetail });
			}
		}

        public static TCommand CollectSandCastleDetail<TCommand>(this ISandGripperRules<TCommand> factory)
           where TCommand : ISandGripperCommand, new()
        {
            return new TCommand { SandGripperAction = SandGripperAction.CollectDetail };
        }

        public static TCommand ReleaseSandCastleDetail<TCommand>(this ISandGripperRules<TCommand> factory)
            where TCommand : ISandGripperCommand, new()
        {
            return new TCommand { SandGripperAction = SandGripperAction.ReleaseDetail };
        }
	}
}
