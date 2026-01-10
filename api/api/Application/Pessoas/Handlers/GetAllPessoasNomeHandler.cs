using api.Application.Pessoas.Queries;
using api.Data;
using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Application.Pessoas.Handlers
{
    public class GetAllPessoasNomeHandler : IQueryHandler<GetAllPessoasNomeQuery, List<PessoaNomeDto>>
    {
        private readonly AppDbContext _db;

        public GetAllPessoasNomeHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<PessoaNomeDto>> HandleAsync(GetAllPessoasNomeQuery query)
        {
            return await _db.Pessoas
               .AsNoTracking()
               .Select(p => new PessoaNomeDto { Id = p.Id, Nome = p.Nome })
               .ToListAsync();
        }
    }
}
