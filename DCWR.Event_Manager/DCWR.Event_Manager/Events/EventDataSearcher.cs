using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Events.Entities;
using DCWR.Event_Manager.Contracts.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DCWR.Event_Manager.Events
{
    public interface IEventDataSearcher
    {
        Task<PagedResponse<EventData>> GetEventData(int pageSize, int pageNumber);
    }

    public class EventDataSearcher : IEventDataSearcher
    {
        private readonly IEventRepository eventRepository;

        public EventDataSearcher(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public async Task<PagedResponse<EventData>> GetEventData(int pageSize, int pageNumber)
        {
            Predicate<Event> predicate = null;
            var pagingInfo = await GetPagingInfo(pageSize, pageNumber, predicate);
            var events = await GetEvents(pageSize, pageNumber, predicate);
            return new PagedResponse<EventData>(
                events.Select(x => x.ToContract()).ToArray(),
                pagingInfo
            );
        }

        private async Task<IReadOnlyCollection<Event>> GetEvents(int pageSize, int pageNumber, Predicate<Event> predicate)
        {
            return await eventRepository
                .GetQuery()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        private async Task<PagingInfo> GetPagingInfo(int pageSize, int pageNumber, Predicate<Event> predicate)
        {
            var query = eventRepository.GetQuery();
            if (predicate != null)
            {
                query = query.Where(x => predicate(x));
            }
            var totalCount = await query.CountAsync();
            return new PagingInfo(pageNumber, pageSize, totalCount);
        }
    }
}
