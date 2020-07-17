using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Utilities;

namespace DCWR.Event_Manager.Events
{
    public interface IEventRepository
    {
        IQueryable<Event> GetQuery();
        Task AddAsync(Event @event);
        //Task<long> GetCount();
        //Task<IReadOnlyCollection<Event>> GetEvents(int pageSize, int pageNumber);
    }

    public class EventRepository: IEventRepository
    {
        public IQueryable<Event> GetQuery()
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Event @event)
        {
            throw new NotImplementedException();
        }
    }
}
