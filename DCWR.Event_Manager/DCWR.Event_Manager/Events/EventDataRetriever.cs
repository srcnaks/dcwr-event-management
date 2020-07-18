using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Events.Entities;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Events
{
    public interface IEventDataRetriever
    {
        Task<PagedResponse<EventData>> GetEventData(int pageSize, int pageNumber);
    }

    public class EventDataRetriever : IEventDataRetriever
    {
        private readonly IEntityRepository<Event> eventRepository;

        public EventDataRetriever(IEntityRepository<Event> eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task<PagedResponse<EventData>> GetEventData(int pageSize, int pageNumber)
        {
            var events = await eventRepository.GetAsync(pageSize, pageNumber);
            var pagingInfo = await GetPagingInfo(pageSize, pageNumber);
            return new PagedResponse<EventData>(
                events.Select(x => x.ToContract()).ToArray(),
                pagingInfo
            );
        }

        private async Task<PagingInfo> GetPagingInfo(int pageSize, int pageNumber)
        {
            var totalCount = await eventRepository.GetCountAsync();
            return new PagingInfo(pageNumber, pageSize, totalCount);
        }
    }
}
