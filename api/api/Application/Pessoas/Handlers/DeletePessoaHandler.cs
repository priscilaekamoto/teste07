using api.Application.Pessoas.Commands;
using api.Data;
using api.Application.Mediator.Interfaces;

namespace api.Application.Pessoas.Handlers
{
    public class DeletePessoaHandler : ICommandHandler<DeletePessoaCommand, bool>
    {
        private readonly AppDbContext _db;
        public DeletePessoaHandler(AppDbContext db) => _db = db;

        public async Task<bool> HandleAsync(DeletePessoaCommand command, CancellationToken cancellationToken)
        {
            var pessoa = await _db.Pessoas.FindAsync(command.Id);
            if (pessoa == null) return false;

            _db.Pessoas.Remove(pessoa);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}