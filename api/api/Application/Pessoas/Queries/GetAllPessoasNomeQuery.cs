using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Pessoas.Queries
{
    public class GetAllPessoasNomeQuery : IQuery<List<PessoaNomeDto>>
    {
        public GetAllPessoasNomeQuery()
        {
        }

        public GetAllPessoasNomeQuery(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
    }
}
