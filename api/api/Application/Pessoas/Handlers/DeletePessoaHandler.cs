using api.Application.Pessoas.Commands;
using api.Data;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Pessoas.Handlers
{
    public class DeletePessoaHandler : ICommandHandler<DeletePessoaCommand, bool>
    {
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _uow;

        public DeletePessoaHandler(AppDbContext db, IUnitOfWork uow)
        {
            _db = db;
            _uow = uow;
        }

        public async Task<bool> HandleAsync(DeletePessoaCommand command, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync(cancellationToken);

            try
            {
                var pessoa = await _db.Pessoas.FindAsync(new object[] { command.Id }, cancellationToken);
                if (pessoa == null)
                {
                    await _uow.RollbackAsync(cancellationToken);
                    return false;
                }

                _db.Pessoas.Remove(pessoa);
                await _uow.SaveChangesAsync(cancellationToken);
                await _uow.CommitAsync(cancellationToken);

                return true;
            }
            catch
            {
                await _uow.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}