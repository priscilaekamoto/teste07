namespace api.Shared.Dtos
{
    public class PessoaDto: ResultDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public int Idade { get; set; }
    }
}