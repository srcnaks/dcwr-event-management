using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Events.Entities;
using DCWR.Event_Manager.Contracts.Events.Queries;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Events.QueryHandlers
{
    public class GetEventsQueryHandler : 
        IQueryHandler<GetEvents,PagedResponse<EventData>>
    {
        private readonly IEventDataSearcher eventDataSearcher;

        public GetEventsQueryHandler(IEventDataSearcher eventDataSearcher)
        {
            this.eventDataSearcher = eventDataSearcher;
        }

        public async Task<PagedResponse<EventData>> HandleAsync(GetEvents query)
        {
            return await eventDataSearcher.GetEventData(query.PageSize,query.PageNumber);
        }
    }
}
