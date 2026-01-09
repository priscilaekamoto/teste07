using api.Application.Mediator.Interfaces;
using api.Application.Pessoas.Dtos;

namespace api.Application.Pessoas.Commands
{
    public sealed class CreatePessoaCommand : ICommand<PessoaDto>
    {
        public string Nome { get; set; } = null!;
        public int Idade { get; set; }
    }
}