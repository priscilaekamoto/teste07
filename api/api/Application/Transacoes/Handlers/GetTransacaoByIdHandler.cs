using api.Application.Transacoes.Queries;
using api.Data;
using api.Models.Enums;
using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Application.Transacoes.Handlers
{
    public class GetTransacaoByIdHandler : IQueryHandler<GetTransacaoByIdQuery, TransacaoDto?>
    {
        private readonly AppDbContext _db;

        public GetTransacaoByIdHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<TransacaoDto?> HandleAsync(GetTransacaoByIdQuery query)
        {
          var  transacao = await _db.Transacoes
                .Where(t => t.Id == query.Id)
                .Select(t => new TransacaoDto
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    Tipo = t.Tipo,
                    Categoria = new CategoriaDto
                    {
                        Id = t.CategoriaId,
                        Descricao = t.Categoria.Descricao
                    },
                    Pessoa = new PessoaDto
                    {
                        Id = t.PessoaId,
                        Nome = t.Pessoa.Nome
                    },
                    Fixo = t.Fixo,
                    Recorrencia = (TipoRecorrencia)t.Recorrencia,
                    DataInicio = t.DataInicio,
                    DataFim = t.DataFim
                })
                .FirstOrDefaultAsync();

            return transacao;
        }
    }
}
