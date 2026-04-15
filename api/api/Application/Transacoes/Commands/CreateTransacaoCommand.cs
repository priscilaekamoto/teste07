using api.Models.Enums;
using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Transacoes.Commands
{
    public sealed class CreateTransacaoCommand : ICommand<TransacaoDto>
    {
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int CategoriaId { get; set; }
        public int PessoaId { get; set; }
        public bool Fixo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public TipoRecorrencia? Recorrencia { get; set; }

    }
}
