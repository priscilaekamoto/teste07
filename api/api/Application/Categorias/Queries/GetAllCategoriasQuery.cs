using api.Application.Dtos;
using api.Application.Mediator.Interfaces;

namespace api.Application.Categorias.Queries
{
    public sealed class GetAllCategoriasQuery : IQuery<List<CategoriaDto>> { }
   
}
