﻿// <copyright file="CommandDispatcher.cs" company="RedTop">
// RedTop
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Engaze.Event.ApplicationService.Core.Command;
using Engaze.Event.ApplicationService.Core.Handler;
using Engaze.Event.Domain.Core.Aggregate;

namespace Engaze.Event.ApplicationService.Core.Dispatcher
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<Type, Func<ICommand, Task>> handlers = new Dictionary<Type, Func<ICommand, Task>>();

        public void Register<TAggregate>(CommandHandler<TAggregate> handler)
            where TAggregate : IEventSourcingAggregate
        {
            var type = typeof(TAggregate);
            if (handlers.ContainsKey(type))
            {
                throw new InvalidOperationException($"Handler exists for type {type}.");
            }

            handlers[type] = command => { return handler.Handle((BaseCommand)command); };
        }

        public async Task Dispatch<TAggregate>(ICommand command)
            where TAggregate : IEventSourcingAggregate
        {
            var type = typeof(TAggregate);
            if (!handlers.ContainsKey(type))
            {
                return;
            }

            var handler = handlers[type];
            await handler(command);
        }
    }
}
