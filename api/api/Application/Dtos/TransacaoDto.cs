using api.Models.Enums;

namespace api.Application.Dtos
{
    public class TransacaoDto:ResultDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }
        public  TipoTransacao Tipo { get; set; }
        public int CategoriaId { get; set; }
        public int PessoaId { get; set; }
    }
}
