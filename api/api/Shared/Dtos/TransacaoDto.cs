using api.Models.Enums;

namespace api.Shared.Dtos
{
    public class TransacaoDto: ResultDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }
        public  TipoTransacao Tipo { get; set; }
        //public int CategoriaId { get; set; }
        public CategoriaDto Categoria { get; set; }
        public PessoaDto Pessoa { get; set; }
        public bool Fixo { get; set; }
        public  TipoRecorrencia Recorrencia { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
