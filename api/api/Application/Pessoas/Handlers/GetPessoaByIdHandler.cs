using api.Application.Pessoas.Queries;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.Application.Mediator.Interfaces;
using api.Application.Dtos;

namespace api.Application.Pessoas.Handlers
{
    public class GetPessoaByIdHandler : IQueryHandler<GetPessoaByIdQuery, PessoaDto?>
    {
        private readonly AppDbContext _db;
        public GetPessoaByIdHandler(AppDbContext db) => _db = db;

        public async Task<PessoaDto?> HandleAsync(GetPessoaByIdQuery query)
        {
            var p = await _db.Pessoas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == query.Id);
            if (p == null) return null;
            return new PessoaDto { Id = p.Id, Nome = p.Nome, Idade = p.Idade };
        }
    }
}