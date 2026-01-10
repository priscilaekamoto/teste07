using api.Shared.Mediator.Interfaces;

namespace api.Shared.Mediator.Dispatcher
{
    public interface IDispatcher
    {
        Task SendAsync(ICommand command, CancellationToken cancellationToken);
        Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken);
        Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken);
    }
}
