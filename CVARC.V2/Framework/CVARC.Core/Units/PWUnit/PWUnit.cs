using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.Core.Units.PWUnit;
using CVARC.V2;

namespace CVARC.Core.Units.PudgeUnit
{
    public class PWUnit : IUnit
    {
        IPWRobot actor;
        IPWRules rules;
        PudgeCommand? PwCommandType;
        public UnitResponse ProcessCommand(object _command)
        {
            var command = Compatibility.Check<IPWCommand>(this, _command);
            PwCommandType = command.Type;
            switch (command.Type)
            {
                case PudgeCommand.Hook:
                    Hook();
                    return UnitResponse.Accepted(0);
                case PudgeCommand.Move:
                    Move();
                    return UnitResponse.Accepted(0);
                case PudgeCommand.RotateClockwise:
                    Rotate(1);
                    return UnitResponse.Accepted(0);
                case PudgeCommand.RotateCounterClockwise:
                    Rotate(-1);
                    return UnitResponse.Accepted(0);
                default:
                    return UnitResponse.Denied();
            }
        }

        private void Rotate(int rotateDestination)
        {
        }

        private void Move()
        {
        }

        private void Hook()
        {
        }
    }
}
