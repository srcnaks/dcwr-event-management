using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Registrations.Entities;
using DCWR.Event_Manager.Contracts.Registrations.Queries;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Registrations.QueryHandlers
{
    public class GetRegistrationsQueryHandler :
        IQueryHandler<GetRegistrations,PagedResponse<AttendeeData>>
    {
        private readonly IAttendeeDataRetriever attendeeDataRetriever;

        public GetRegistrationsQueryHandler(IAttendeeDataRetriever attendeeDataRetriever)
        {
            this.attendeeDataRetriever = attendeeDataRetriever;
        }

        public async Task<PagedResponse<AttendeeData>> HandleAsync(GetRegistrations query)
        {
            return await attendeeDataRetriever
                .GetAttendeeDataOfEvent(query.EventId, query.PageSize, query.PageNumber);
        }
    }
}
