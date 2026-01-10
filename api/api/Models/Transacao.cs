using api.Application.Dtos;
using api.Models.Enums;

namespace api.Models
{
    public class Transacao: ResultDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }
        public TipoTransacao Tipo { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; } = null!;

        // Caso o usuário informe um menor de idade (menor de 18), apenas despesas deverão ser aceitas.
        public bool Menorde18anosEReceita(Pessoa pessoa)
        {
            if (pessoa.Idade < 18 && Tipo == TipoTransacao.Receita)
            {
                this.Message = "Pessoas menores de 18 anos não podem registrar transações do tipo Receita.";
                this.Code = StatusCodes.Status400BadRequest;
                return true;
            }
            return false;
        }
    }
}
