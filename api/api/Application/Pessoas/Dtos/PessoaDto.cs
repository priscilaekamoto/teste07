namespace api.Application.Pessoas.Dtos
{
    public class PessoaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public int Idade { get; set; }
    }
}