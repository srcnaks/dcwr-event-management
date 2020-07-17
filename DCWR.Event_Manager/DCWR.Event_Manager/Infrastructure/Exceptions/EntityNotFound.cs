using System;
using System.Collections.Generic;
using System.Text;

namespace DCWR.Event_Manager.Infrastructure.Exceptions
{
    public class EntityNotFound : Exception
    {
        public string EntityName { get; }
        public string EntityId { get; }

        public EntityNotFound(string entityName, Guid entityId) : base($"{ entityName } with id: { entityId } does not exist.")
        {
            EntityName = entityName;
            EntityId = entityId.ToString();
        }

        public static EntityNotFound Create<TEntity>(Guid entityId) => new EntityNotFound(typeof(TEntity).Name, entityId);
    }
}
