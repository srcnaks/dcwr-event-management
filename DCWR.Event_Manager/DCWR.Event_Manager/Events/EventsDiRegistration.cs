using DCWR.Event_Manager.Contracts.Events.Commands;
using DCWR.Event_Manager.Contracts.Events.Entities;
using DCWR.Event_Manager.Contracts.Events.Queries;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Events.CommandHandlers;
using DCWR.Event_Manager.Events.QueryHandlers;
using DCWR.Event_Manager.Events.Validators;
using DCWR.Event_Manager.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DCWR.Event_Manager.Events
{
    public class EventsDiRegistration : IDiRegistration
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IEventBuilder, EventBuilder>();
            serviceCollection.AddScoped<IEntityRepository<Event>, EntityRepository<Event>>();
            serviceCollection.AddScoped<ICreateEventValidator, CreateEventValidator>();
            serviceCollection.AddScoped<ICommandHandler<CreateEvent>, CreateEventHandler>();
            serviceCollection.AddScoped<IEventDataRetriever, EventDataRetriever>();
            serviceCollection.AddScoped<IQueryHandler<GetEvents, PagedResponse<EventData>>, GetEventsQueryHandler>();
        }
    }
}
