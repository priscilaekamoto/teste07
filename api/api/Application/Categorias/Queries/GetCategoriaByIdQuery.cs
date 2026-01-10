using api.Application.Dtos;
using api.Application.Mediator.Interfaces;

namespace api.Application.Categorias.Queries
{
    public sealed class GetCategoriaByIdQuery : IQuery<CategoriaDto?>
    {
        public GetCategoriaByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
