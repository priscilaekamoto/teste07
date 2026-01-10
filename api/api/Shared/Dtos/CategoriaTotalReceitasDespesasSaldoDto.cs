namespace api.Shared.Dtos
{
    public class CategoriaTotalReceitasDespesasSaldoDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal TotalReceita { get; set; }
        public decimal TotalDespesa { get; set; }
        public decimal Saldo { get; set; }
    }
}
