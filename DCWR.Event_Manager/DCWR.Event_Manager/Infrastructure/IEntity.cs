using System;

namespace DCWR.Event_Manager.Infrastructure
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}