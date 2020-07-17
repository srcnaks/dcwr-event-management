using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DCWR.Event_Manager.Infrastructure
{
    public class InfrastructureDiRegistration : IDiRegistration
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            serviceCollection.AddSingleton<IGuidIdGenerator>(GuidIdGenerator.Instance);
            serviceCollection.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
        }
    }
}
