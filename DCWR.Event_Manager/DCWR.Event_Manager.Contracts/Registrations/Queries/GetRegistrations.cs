using System;
using DCWR.Event_Manager.Contracts.Registrations.Entities;
using DCWR.Event_Manager.Contracts.Utilities;

namespace DCWR.Event_Manager.Contracts.Registrations.Queries
{
    public class GetRegistrations : IPagedQuery<PagedResponse<AttendeeData>>
    {
        public Guid EventId { get; }
        public int PageSize { get; }
        public int PageNumber { get; }

        public GetRegistrations(
            Guid eventId, 
            int pageSize, 
            int pageNumber)
        {
            EventId = eventId;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
