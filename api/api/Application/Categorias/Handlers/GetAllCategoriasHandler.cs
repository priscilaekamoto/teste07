using api.Application.Categorias.Queries;
using api.Data;
using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Application.Categorias.Handlers
{
    public class GetAllCategoriasHandler : IQueryHandler<GetAllCategoriasQuery, List<CategoriaDto>>
    {
        private readonly AppDbContext _db;

        public GetAllCategoriasHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<CategoriaDto>> HandleAsync(GetAllCategoriasQuery query)
        {
           return await _db.Categorias
                .AsNoTracking()
                .Select(c => new CategoriaDto 
                { 
                    Id = c.Id, 
                    Descricao = c.Descricao, 
                    Finalidade = c.Finalidade 
                })
                .ToListAsync();
        }
    }
}
