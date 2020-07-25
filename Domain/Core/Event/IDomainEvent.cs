﻿// <copyright file="IDomainEvent.cs" company="RedTop">
// RedTop
// </copyright>

namespace Engaze.EventSourcing.Core
{
    using System;
    using Newtonsoft.Json;

    public interface IDomainEvent
    {
        /// <summary>
        /// The event identifier
        /// </summary>
        Guid EventId { get; }

        /// <summary>
        /// The identifier of the aggregate which has generated the event
        /// </summary>
        Guid AggregateId { get; }

        /// <summary>
        /// The version of the aggregate when the event has been generated
        /// </summary>
        long AggregateVersion { get; }

        [JsonIgnore]
        string AggregateIdAsString { get; }
    }
}
