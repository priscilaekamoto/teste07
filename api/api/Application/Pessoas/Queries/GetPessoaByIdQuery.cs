using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Pessoas.Queries
{
    public sealed class GetPessoaByIdQuery : IQuery<PessoaDto?>
    {
        public int Id { get; set; }
        public GetPessoaByIdQuery(int id) => Id = id;
    }
}