using api.Application.Pessoas.Dtos;
using api.Application.Mediator.Interfaces;

namespace api.Application.Pessoas.Queries
{
    public sealed class GetAllPessoasQuery : IQuery<List<PessoaDto>> { }
}