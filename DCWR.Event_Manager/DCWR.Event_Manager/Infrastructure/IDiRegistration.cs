using Microsoft.Extensions.DependencyInjection;

namespace DCWR.Event_Manager.Infrastructure
{
    public interface IDiRegistration
    {
        void Register(IServiceCollection serviceCollection);
    }
}