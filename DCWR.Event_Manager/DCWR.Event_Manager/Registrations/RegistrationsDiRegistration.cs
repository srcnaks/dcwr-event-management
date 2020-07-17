using DCWR.Event_Manager.Contracts.Registrations.Commands;
using DCWR.Event_Manager.Contracts.Registrations.Entities;
using DCWR.Event_Manager.Contracts.Registrations.Queries;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.Registrations.CommandHandlers;
using DCWR.Event_Manager.Registrations.QueryHandlers;
using DCWR.Event_Manager.Registrations.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace DCWR.Event_Manager.Registrations
{
    public class RegistrationsDiRegistration : IDiRegistration
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICommandHandler<RegisterToEvent>, RegisterToEventCommandHandler>();
            serviceCollection.AddScoped<IRegisterToEventValidator, RegisterToEventValidator>();
            serviceCollection.AddScoped<IRegistrationBuilder, RegistrationBuilder>();
            serviceCollection.AddScoped<IEntityRepository<Registration>, EntityRepository<Registration>>();
            serviceCollection.AddScoped<IQueryHandler<GetRegistrations, PagedResponse<AttendeeData>>, GetRegistrationsQueryHandler>();
            serviceCollection.AddScoped<IAttendeeDataRetriever, AttendeeDataRetriever>();
        }
    }
}
