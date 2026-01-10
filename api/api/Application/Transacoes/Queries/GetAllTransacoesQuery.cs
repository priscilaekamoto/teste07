using api.Application.Dtos;
using api.Application.Mediator.Interfaces;

namespace api.Application.Transacoes.Queries
{
    public sealed class GetAllTransacoesQuery : IQuery<List<TransacaoDto>>
    {
    }
}
