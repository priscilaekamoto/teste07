using api.Application.Mediator.Interfaces;
using api.Application.Transacoes.Commands;
using api.Application.Transacoes.Dtos;
using api.Data;
using api.Models;

namespace api.Application.Transacoes.Handlers
{
    public class CreateTransacaoHandler : ICommandHandler<CreateTransacaoCommand, TransacaoDto>
    {
        private readonly AppDbContext _db;
        private readonly IUnitOfWork _uow;

        public CreateTransacaoHandler(AppDbContext db, IUnitOfWork uow)
        {
            _db = db;
            _uow = uow;
        }

        public async Task<TransacaoDto> HandleAsync(CreateTransacaoCommand command, CancellationToken cancellationToken)
        {
            await _uow.BeginTransactionAsync(cancellationToken);

            try
            {
                var transacao = new Transacao
                {
                    Descricao = command.Descricao,
                    Valor = command.Valor,
                    Tipo = command.Tipo,
                    CategoriaId = command.CategoriaId,
                    PessoaId = command.PessoaId

                };

                _db.Transacoes.Add(transacao);
                await _uow.SaveChangesAsync(cancellationToken);
                await _uow.CommitAsync(cancellationToken);

                return new TransacaoDto { Id = transacao.Id, Descricao = transacao.Descricao, Valor = transacao.Valor, Tipo = transacao.Tipo, CategoriaId = transacao.CategoriaId, PessoaId = transacao.PessoaId };
            } catch
            {
                await _uow.RollbackAsync(cancellationToken);
                throw;
            }
                       
        }
    }
}
