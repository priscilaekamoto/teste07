using api.Application.Mediator.Interfaces;
using api.Application.Transacoes.Dtos;

namespace api.Application.Transacoes.Queries
{
    public sealed class GetAllTransacoesQuery : IQuery<List<TransacaoDto>>
    {
    }
}
