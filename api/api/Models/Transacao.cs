using api.Models.Enums;
using api.Shared.Dtos;

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

        public ResultDto Valid(Pessoa pessoa, Categoria categoria) 
        {
            List<string> messages = new List<string>();
            var code = StatusCodes.Status200OK;

            // Caso o usuário informe um menor de idade (menor de 18), apenas despesas deverão ser aceitas.
            if (pessoa.Idade < 18 && Tipo == TipoTransacao.Receita)
            {
                messages.Add("Pessoas menores de 18 anos não podem registrar transações do tipo Receita.");
                code = StatusCodes.Status400BadRequest;
            }

            // A finalidade da categoria deve corresponder ao tipo da transação.
            if ((int)categoria.Finalidade != (int)Tipo)
            {
                messages.Add("Finalidade da Categoria diferente do Tipo passado.");
                code = StatusCodes.Status400BadRequest;
            }

            return new ResultDto { Messages = messages, Code = code };
        }
    }
}
