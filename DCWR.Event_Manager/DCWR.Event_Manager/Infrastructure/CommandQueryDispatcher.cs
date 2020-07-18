using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Utilities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DCWR.Event_Manager.Infrastructure
{
    public interface ICommandQueryDispatcher
    {
        Task DispatchAsync(ICommand command);

        Task<TResponse> DispatchAsync<TResponse, TQuery>(TQuery query)
            where TQuery : class, IQuery<TResponse>;
    }

    public class CommandQueryDispatcher : ICommandQueryDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandQueryDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task DispatchAsync(ICommand command)
        {
            var handler = GetCommandHandler(command);
            return handler.HandleAsync(command);
        }

        public Task<TResponse> DispatchAsync<TResponse, TQuery>(TQuery query) where TQuery : class, IQuery<TResponse>
        {
            var handler = GetQueryHandler<TQuery,TResponse>(query);
            return handler.HandleAsync(query);
        }


        private ICommandHandler<T> GetCommandHandler<T>(T command) where T : ICommand
        {
            //var generic = typeof(ICommandHandler<>);
            //Type constructed = generic.MakeGenericType(command.GetType());
            //var handler = serviceProvider.GetService(constructed) as ICommandHandler<T>;
            return serviceProvider.GetService<ICommandHandler<T>>();
        }

        private IQueryHandler<TQuery,TResponse> GetQueryHandler<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
        {
            //var queryHandlerType = typeof(IQueryHandler<,>);
            //var genericHandlerType = queryHandlerType.MakeGenericType(typeof(TResponse), typeof(TQuery));
            //var handler = serviceProvider.GetService(genericHandlerType);
            return serviceProvider.GetService<IQueryHandler<TQuery, TResponse>>();
        }
    }
}
