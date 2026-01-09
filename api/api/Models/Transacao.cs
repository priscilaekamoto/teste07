using api.Models.Enums;

namespace api.Models
{
    public class Transacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; } = null!;

    }
}
