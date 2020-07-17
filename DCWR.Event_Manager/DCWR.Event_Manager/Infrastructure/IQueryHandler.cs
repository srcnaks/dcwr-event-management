using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Utilities;

namespace DCWR.Event_Manager.Infrastructure
{
    public interface IQueryHandler<in TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}