using System;
using System.Linq;
using DCWR.Event_Manager.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DCWR.Event_Manager
{
    public class Bootstrap
    {
        private readonly IServiceCollection serviceCollection;
        public Bootstrap(IServiceCollection serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        public IServiceCollection RunDiRegistrations()
        {
            var registrations = this.GetType().Assembly
                .GetTypes()
                .Where(type =>
                    !type.IsAbstract &&
                    typeof(IDiRegistration).IsAssignableFrom(type)
                );

            foreach (var registrationType in registrations)
            {
                var registration = (IDiRegistration)Activator.CreateInstance(registrationType);
                registration.Register(serviceCollection);
            }

            return serviceCollection;
        }
    }
}
