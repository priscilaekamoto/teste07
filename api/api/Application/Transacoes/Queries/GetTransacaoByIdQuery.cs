using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

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
