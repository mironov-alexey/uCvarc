using System;
using AIRLab.Mathematics;
using CVARC.Core.Units.PudgeUnit;
using CVARC.V2;

namespace CVARC.Core.Units.PWUnit
{
    public class PWUnit : IUnit
    {
        IPWRobot actor;

        IPWRules rules;
        public PWUnit(IActor actor)
        {
            actor = Compatibility.Check<IPWRobot>(this, actor);
//            actor.World.Clocks.AddTrigger(new TimerTrigger(UpdateSpeed, TriggerFrequency)); adding trigger example
            rules = Compatibility.Check<IPWRules>(this, actor.Rules);
            
        }

        public UnitResponse ProcessCommand(object _command)
        {
            var command = Compatibility.Check<IPWCommand>(this, _command);
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
            var angular = rotateDestination * rules.RotationAngle.Radian;
            var speed = new Frame3D(
                0,
                0,
                0,
                Angle.Zero,
                Angle.FromRad(angular),
                Angle.Zero);
            actor.World.Engine.SetSpeed(actor.ObjectId, speed);
        }

        private void Move()
        {
            var location = actor.World.Engine.GetAbsoluteLocation(actor.ObjectId);
            var linear = rules.MovementRange/rules.MovementTime;
            var angle = location.Yaw.Radian;
            var speed = new Frame3D(
                linear * Math.Cos(angle),
                linear * Math.Sin(angle),
                0,
                Angle.Zero,
                Angle.Zero,
                Angle.Zero);
            actor.World.Engine.SetSpeed(actor.ObjectId, speed);
        }

        private void Hook()
        {
        }
    }
}
