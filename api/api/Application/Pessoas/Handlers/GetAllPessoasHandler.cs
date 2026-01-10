using api.Application.Pessoas.Queries;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Application.Mediator.Interfaces;
using api.Application.Dtos;

namespace api.Application.Pessoas.Handlers
{
    public class GetAllPessoasHandler : IQueryHandler<GetAllPessoasQuery, List<PessoaDto>>
    {
        private readonly AppDbContext _db;
        public GetAllPessoasHandler(AppDbContext db) => _db = db;

        public async Task<List<PessoaDto>> HandleAsync(GetAllPessoasQuery query)
        {
            return await _db.Pessoas
                .AsNoTracking()
                .Select(p => new PessoaDto { Id = p.Id, Nome = p.Nome, Idade = p.Idade })
                .ToListAsync();
        }
    }
}