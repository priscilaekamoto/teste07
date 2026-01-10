using api.Application.Mediator.Interfaces;
using api.Application.Dtos;

namespace api.Application.Pessoas.Queries
{
    public sealed class GetAllPessoasQuery : IQuery<List<PessoaDto>> { }
}