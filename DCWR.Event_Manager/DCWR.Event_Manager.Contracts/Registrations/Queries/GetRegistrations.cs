using System;
using DCWR.Event_Manager.Contracts.Registrations.Entities;
using DCWR.Event_Manager.Contracts.Utilities;

namespace DCWR.Event_Manager.Contracts.Registrations.Queries
{
    public class GetRegistrations : IQuery<PagedResponse<AttendeeData>>
    {
        public Guid EventId { get; }

        public GetRegistrations(Guid eventId)
        {
            EventId = eventId;
        }
    }
}
