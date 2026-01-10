using api.Shared.Dtos;
using api.Shared.Mediator.Interfaces;

namespace api.Application.Categorias.Queries
{
    public class GetAllTotalReceitasdespesasSaldoCategoriaQuery : IQuery<List<CategoriaTotalReceitasDespesasSaldoDto>>
    {
        public GetAllTotalReceitasdespesasSaldoCategoriaQuery()
        {
        }

        public GetAllTotalReceitasdespesasSaldoCategoriaQuery(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public  int Id { get; set; }
        public string Descricao { get; set; } = null!;
    }
}
