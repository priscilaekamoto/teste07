using api.Application.Categorias.Dtos;
using api.Application.Mediator.Interfaces;
using api.Models.Enums;

namespace api.Application.Categorias.Commands
{
    public sealed class CreateCategoriaCommand : ICommand<CategoriaDto>
    {
        public string Descricao { get; set; } = null!;
        public Finalidade Finalidade { get; set; }
    }
}
