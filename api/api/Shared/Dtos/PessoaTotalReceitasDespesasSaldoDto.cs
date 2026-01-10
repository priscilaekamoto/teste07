using api.Models.Enums;

namespace api.Shared.Dtos
{
    public class PessoaTotalReceitaDespesasSaldoDto
    {
        public  int Id { get; set; }
        public string Nome { get; set; } = null!;
        public decimal TotalReceita { get; set; }
        public decimal TotalDespesa { get; set; }
        public decimal Saldo { get; set; }
    }
}
