using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts;
using DCWR.Event_Manager.Contracts.Utilities;

namespace DCWR.Event_Manager.Infrastructure
{
    public interface ICommandHandler<in TRequest> where TRequest : ICommand
    {
        Task HandleAsync(TRequest command);
    }
}