using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Pessoas.Queries
{
    public sealed class GetAllPessoasQuery : IQuery<List<PessoaDto>> { }
}