using api.Application.Mediator.Interfaces;
using api.Application.Pessoas.Commands;
using api.Application.Pessoas.Dtos;
using api.Data;
using api.Models;

namespace api.Application.Pessoas.Handlers
{
    public class CreatePessoaHandler : ICommandHandler<CreatePessoaCommand, PessoaDto>
    {
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _uow;

        public CreatePessoaHandler(AppDbContext db, IUnitOfWork uow)
        {
            _db = db;
            _uow = uow;
        }

        public async Task<PessoaDto> HandleAsync(CreatePessoaCommand command, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync(cancellationToken);

            try
            {
                var pessoa = new Pessoa
                {
                    Nome = command.Nome,
                    Idade = command.Idade
                };

                _db.Pessoas.Add(pessoa);
                await _uow.SaveChangesAsync(cancellationToken);
                await _uow.CommitAsync(cancellationToken);

                return new PessoaDto { Id = pessoa.Id, Nome = pessoa.Nome, Idade = pessoa.Idade };
            }
            catch
            {
                await _uow.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}