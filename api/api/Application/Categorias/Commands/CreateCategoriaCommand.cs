using api.Models.Enums;
using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Categorias.Commands
{
    public sealed class CreateCategoriaCommand : ICommand<CategoriaDto>
    {
        public string Descricao { get; set; } = null!;
        public Finalidade Finalidade { get; set; }
    }
}
