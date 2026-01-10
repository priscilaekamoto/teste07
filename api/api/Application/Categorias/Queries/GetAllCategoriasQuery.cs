using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Categorias.Queries
{
    public sealed class GetAllCategoriasQuery : IQuery<List<CategoriaDto>> { }
   
}
