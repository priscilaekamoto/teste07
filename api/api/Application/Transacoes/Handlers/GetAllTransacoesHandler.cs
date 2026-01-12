using api.Application.Transacoes.Queries;
using api.Data;
using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Application.Transacoes.Handlers
{
    public class GetAllTransacoesHandler : IQueryHandler<GetAllTransacoesQuery, List<TransacaoDto>>
    {
        private readonly AppDbContext _db;

        public GetAllTransacoesHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TransacaoDto>> HandleAsync(GetAllTransacoesQuery query)
        {
            return await _db.Transacoes
                .AsNoTracking()
                .Select
                (t => new TransacaoDto
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    Tipo = t.Tipo,
                    Categoria = new CategoriaDto() { Descricao = t.Categoria.Descricao, Id = t.Categoria.Id},
                    Pessoa = new PessoaDto() { Nome = t.Pessoa.Nome, Id = t.Pessoa.Id }
                }) .ToListAsync();
        }
    }
}
