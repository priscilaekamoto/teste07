namespace api.Shared.Mediator.Interfaces;

public interface IQueryHandler<TQuery, TResponse>

    where TQuery : IQuery<TResponse>
{
    Task<TResponse> HandleAsync(TQuery query);
}
