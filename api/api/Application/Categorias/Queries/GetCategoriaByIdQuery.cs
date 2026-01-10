using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

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
