using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Pessoas.Queries
{
    public class GetAllTotalReceitasDespesasSaldoPessoasQuery : IQuery<List<PessoaTotalReceitaDespesasSaldoDto>>
    {
        public GetAllTotalReceitasDespesasSaldoPessoasQuery()
        {
        }

        public GetAllTotalReceitasDespesasSaldoPessoasQuery(int id, string nome)
        {
            Id = id;
            Nome = nome;
           
        }

        public int Id { get; set; }
        public string Nome { get; set; } = null!;
    }
}
