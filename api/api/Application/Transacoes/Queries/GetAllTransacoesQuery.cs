using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Transacoes.Queries
{
    public sealed class GetAllTransacoesQuery : IQuery<List<TransacaoDto>>
    {
    }
}
