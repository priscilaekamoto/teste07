using api.Application.Mediator.Interfaces;
using api.Application.Transacoes.Dtos;
using api.Models.Enums;

namespace api.Application.Transacoes.Commands
{
    public sealed class CreateTransacaoCommand : ICommand<TransacaoDto>
    {
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int CategoriaId { get; set; }
        public int PessoaId { get; set; }

    }
}
