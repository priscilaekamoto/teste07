using api.Application.Mediator.Interfaces;
using System.ComponentModel.DataAnnotations;
using api.Application.Pessoas.Dtos;

namespace api.Application.Pessoas.Commands
{
    public sealed class CreatePessoaCommand : ICommand<PessoaDto>
    {
        [Required]
        public string Nome { get; set; } = null!;

        [Range(0, int.MaxValue)]
        public int Idade { get; set; }
    }
}