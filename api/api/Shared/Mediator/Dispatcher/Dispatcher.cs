using api.Data;
using api.Shared.Mediator.Interfaces;

namespace api.Shared.Mediator.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AppDbContext _context;
        
        public Dispatcher(IServiceProvider serviceProvider, AppDbContext context)
        {
            _serviceProvider = serviceProvider;
            _context = context;
        }

        public async Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
        {
            var queryType = query.GetType();
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResponse));
            var handler = _serviceProvider.GetService(handlerType);

            if (handler is null)
                throw new InvalidOperationException($"Handler not found for query type {queryType.Name}");

            var methodInfo = handlerType.GetMethod("HandleAsync");

            if (methodInfo is null)
                throw new InvalidOperationException($"Method HandleAsync not found in handler type {handlerType.Name}");

            var parameters = methodInfo.GetParameters();

            object[] args = parameters.Length == 2
                ? new object[] { query, cancellationToken }
                : new object[] { query };

            var result = methodInfo.Invoke(handler, args);

            if (result is not Task<TResponse> task)
                throw new InvalidOperationException("Handler did not return the expected Task<TResponse>.");

            return await task;

        }

        public async Task SendAsync(
            ICommand command,
            CancellationToken cancellationToken = default)
        {
            var commandType = command.GetType();
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
            var handler = _serviceProvider.GetService(handlerType);

            if (handler is null)
                throw new InvalidOperationException($"Handler not found for command type {commandType.Name}");

            var methodInfo = handlerType.GetMethod("HandleAsync");

            if (methodInfo is null)
                throw new InvalidOperationException($"Method HandleAsync not found in handler type {handlerType.Name}");

            var parameters = methodInfo.GetParameters();

            object[] args = parameters.Length == 2
                ? new object[] { command, cancellationToken }
                : new object[] { command };

            var result = methodInfo.Invoke(handler, args);

            if (result is not Task task)
                throw new InvalidOperationException("Handler did not return the expected Task.");

            await task;
        }

        public async Task<TResponse> SendAsync<TResponse>(
            ICommand<TResponse> command,
            CancellationToken cancellationToken = default)
        {
            var commandType = command.GetType();
            var handlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TResponse));
            var handler = _serviceProvider.GetService(handlerType);

            if (handler is null)
                throw new InvalidOperationException($"Handler not found for command type {commandType.Name}");

            var methodInfo = handlerType.GetMethod("HandleAsync");

            if (methodInfo is null)
                throw new InvalidOperationException($"Method HandleAsync not found in handler type {handlerType.Name}");

            var parameters = methodInfo.GetParameters();

            object[] args = parameters.Length == 2
                ? new object[] { command, cancellationToken }
                : new object[] { command };

            var result = methodInfo.Invoke(handler, args);

            if (result is not Task<TResponse> task)
                throw new InvalidOperationException("Handler did not return the expected Task<TResponse>.");

            return await task;
        }
       
    }
}
