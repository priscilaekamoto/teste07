namespace api.Shared.Mediator.Interfaces;

public interface ICommandHandler<TCommand>

    where TCommand : ICommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<TCommand, TRespponse>

    where TCommand : ICommand<TRespponse>
{
    Task<TRespponse> HandleAsync(TCommand command, CancellationToken cancellationToken); 
}

