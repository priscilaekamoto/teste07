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
                if(command.Valor < 0)
                    return new TransacaoDto() { Code = StatusCodes.Status400BadRequest, Messages = new List<string> { "Valor deve ser positivo!" } };

                var transacao = new Transacao
                {
                    Descricao = command.Descricao,
                    Valor = command.Valor,
                    Tipo = command.Tipo,
                    CategoriaId = command.CategoriaId,
                    PessoaId = command.PessoaId,
                    Fixo = command.Fixo,
                    Recorrencia = command.Recorrencia,
                    DataInicio = command.DataInicio,
                    DataFim = command.DataFim

                };

                var pessoa =  await _db.Pessoas.FindAsync(command.PessoaId);
                var categoria = await _db.Categorias.FindAsync(command.CategoriaId);

                if (pessoa == null)
                    return new TransacaoDto() { Code = StatusCodes.Status400BadRequest, Messages = new List<string> { "Pessoal não encontrada!" } };

                if (categoria == null)
                    return new TransacaoDto() { Code = StatusCodes.Status400BadRequest, Messages = new List<string> { "Categoria não encontrada!" } };

                var ret = transacao.Valid(pessoa, categoria);

                if (pessoa != null && ret.Code != StatusCodes.Status200OK)
                {
                    return new TransacaoDto
                    {
                        Categoria = new CategoriaDto() { Descricao = categoria.Descricao, Id = categoria.Id },
                        Descricao = command.Descricao,
                        Pessoa = new PessoaDto() {Id = pessoa.Id, Nome = pessoa.Nome },
                        Tipo = command.Tipo,
                        Valor = command.Valor,
                        Fixo = command.Fixo,
                        Recorrencia = (Models.Enums.TipoRecorrencia)command.Recorrencia,
                        DataInicio = command.DataInicio,
                        DataFim = command.DataFim,
                        Messages = ret.Messages,
                        Code = ret.Code
                    };
                }

                await _db.Transacoes.AddAsync(transacao);
                await _uow.SaveChangesAsync(cancellationToken);
                await _uow.CommitAsync(cancellationToken);

                return new TransacaoDto { 
                    Id = transacao.Id, 
                    Descricao = transacao.Descricao, 
                    Valor = transacao.Valor, 
                    Tipo = transacao.Tipo, 
                    Categoria = new CategoriaDto() { Descricao = transacao.Categoria.Descricao, Id = transacao.Categoria.Id }, 
                    Pessoa = new PessoaDto() { Nome = transacao.Pessoa.Nome, Id = transacao.Pessoa.Id },
                    Fixo = transacao.Fixo,
                    Recorrencia = (Models.Enums.TipoRecorrencia)transacao.Recorrencia,
                    DataInicio = transacao.DataInicio,
                    DataFim = transacao.DataFim
                };

            } catch
            {
                await _uow.RollbackAsync(cancellationToken);
                throw;
            }
                       
        }
    }
}
