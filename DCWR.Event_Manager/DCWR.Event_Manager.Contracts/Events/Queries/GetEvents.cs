using DCWR.Event_Manager.Contracts.Events.Entities;
using DCWR.Event_Manager.Contracts.Utilities;

namespace DCWR.Event_Manager.Contracts.Events.Queries
{
    public class GetEvents : IPagedQuery<PagedResponse<EventData>>
    {
        public int PageSize { get; }
        public int PageNumber { get; }

        public GetEvents(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }
}
