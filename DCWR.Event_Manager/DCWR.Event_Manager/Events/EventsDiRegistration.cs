using System;
using DCWR.Event_Manager.Contracts.Events.Commands;
using DCWR.Event_Manager.Events.CommandHandlers;
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
            serviceCollection.AddScoped<IEventRepository, EventRepository>();
            serviceCollection.AddScoped<ICreateEventValidator, CreateEventValidator>();
            serviceCollection.AddScoped<ICommandHandler<CreateEvent>, CreateEventHandler>();
        }
    }
}
