﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CVARC.V2
{
    class ReflectableTestBuilder<TSensorData, TCommand, TWorldState, TWorld>
        : TestBuilder<TSensorData, TCommand, TWorldState, TWorld>
        where TWorld : IWorld
        where TCommand : class, ISimpleMovementCommand
        where TWorldState : IWorldState
        where TSensorData : class, new()
    {
        public override void AddCommand(TCommand command)
        {
            CurrentTest.Add(new ReflectableTestAction<TSensorData, TCommand>(command));
        }

        public override void AddAssert(ISensorAsserter<TSensorData> assert)
        {
            CurrentTest.Add(new ReflectableTestAction<TSensorData, TCommand>(assert));
        }

        public override void AddAssert(Action<TSensorData, IAsserter> assert)
        {
            CurrentTest.Add(new ReflectableTestAction<TSensorData, TCommand>(new DelegatedAsserter<TSensorData>(assert)));
        }

        public CvarcTest<TSensorData, TCommand, TWorld, TWorldState> CreateTest
            (TWorldState worldState, SettingsProposal settings, bool reflected)
        {
            if (reflected)
            {
                foreach (var act in CurrentTest.Where(act => act is ReflectableTestAction<TSensorData, TCommand>))
                    ((ReflectableTestAction<TSensorData, TCommand>)act).Reflect();

                settings = SettingsProposal.DeepCopy(settings);
                settings.Controllers = Reflect(settings.Controllers);
            }
            return base.CreateTest(worldState, settings);            
        }
        
        private List<ControllerSettings> Reflect(List<ControllerSettings> controllers)
        {
            return controllers
                .Select(x => new ControllerSettings()
                {
                    ControllerId = ReflectControllerId(x.ControllerId),
                    Name = x.Name,
                    Type = x.Type,
                })
                .ToList();
        }

        private string ReflectControllerId(string id)
        {
            if (id == TwoPlayersId.Left)
                return TwoPlayersId.Right;
            if (id == TwoPlayersId.Right)
                return TwoPlayersId.Left;
            throw new ArgumentException("Invalid two players id!");                    
        }
    }
}
