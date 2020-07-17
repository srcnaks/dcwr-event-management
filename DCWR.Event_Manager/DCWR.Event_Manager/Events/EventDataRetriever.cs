using System;
using System.Linq;
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
            Predicate<Event> predicate = null;
            var events = await eventRepository.GetAsync(pageSize, pageNumber, predicate);
            var pagingInfo = await GetPagingInfo(pageSize, pageNumber, predicate);
            return new PagedResponse<EventData>(
                events.Select(x => x.ToContract()).ToArray(),
                pagingInfo
            );
        }

        private async Task<PagingInfo> GetPagingInfo(int pageSize, int pageNumber, Predicate<Event> predicate)
        {
            var totalCount = await eventRepository.GetCount(predicate);
            return new PagingInfo(pageNumber, pageSize, totalCount);
        }
    }
}
