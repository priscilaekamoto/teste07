using api.Application.Transacoes.Commands;
using api.Data;
using api.Models;
using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

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

                var pessoa = _db.Pessoas.Find(command.PessoaId);
                var categoria = _db.Categorias.Find(command.CategoriaId);

                if (pessoa == null)
                    return new TransacaoDto() { Code = StatusCodes.Status400BadRequest, Messages = new List<string> { "Pessoal não encontrada!" } };

                if (categoria == null)
                    return new TransacaoDto() { Code = StatusCodes.Status400BadRequest, Messages = new List<string> { "Categoria não encontrada!" } };

                var ret = transacao.Valid(pessoa, categoria);

                if (pessoa != null && ret.Code != StatusCodes.Status200OK)
                {
                    return new TransacaoDto
                    {
                        CategoriaId = command.CategoriaId,
                        Descricao = command.Descricao,
                        PessoaId = command.PessoaId,
                        Tipo = command.Tipo,
                        Valor = command.Valor,
                        Messages = ret.Messages,
                        Code = ret.Code
                    };
                }

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
