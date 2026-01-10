using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Pessoas.Commands
{
    public sealed class CreatePessoaCommand : ICommand<PessoaDto>
    {
        public string Nome { get; set; } = null!;
        public int Idade { get; set; }
    }
}