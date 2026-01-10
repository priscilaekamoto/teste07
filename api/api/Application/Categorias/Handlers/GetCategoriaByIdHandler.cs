using api.Application.Categorias.Queries;
using api.Application.Dtos;
using api.Application.Mediator.Interfaces;
using api.Data;
using Microsoft.EntityFrameworkCore;

namespace api.Application.Categorias.Handlers
{
    public class GetCategoriaByIdHandler : IQueryHandler<GetCategoriaByIdQuery, CategoriaDto?>
    {
        private readonly AppDbContext _db;

        public GetCategoriaByIdHandler(AppDbContext db)
        {
            _db = db;
        }

        public async Task<CategoriaDto?> HandleAsync(GetCategoriaByIdQuery query)
        {
            var c = await _db.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == query.Id);
            if (c == null) return null;
            return new CategoriaDto { Id = c.Id, Descricao = c.Descricao, Finalidade = c.Finalidade };
        }
    }
}
