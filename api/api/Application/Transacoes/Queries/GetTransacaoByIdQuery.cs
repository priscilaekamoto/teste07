using api.Application.Mediator.Interfaces;
using api.Application.Transacoes.Dtos;

namespace api.Application.Transacoes.Queries
{
    public sealed class GetTransacaoByIdQuery : IQuery<TransacaoDto?>
    {
        public GetTransacaoByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
