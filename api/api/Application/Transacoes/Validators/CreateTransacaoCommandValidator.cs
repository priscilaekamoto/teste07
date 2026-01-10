using api.Application.Transacoes.Commands;
using api.Models.Enums;
using FluentValidation;

namespace api.Application.Transacoes.Validators
{
    public class CreateTransacaoCommandValidator : AbstractValidator<CreateTransacaoCommand>
    {
        public CreateTransacaoCommandValidator() {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória.")
                .MaximumLength(500);

            RuleFor(x => x.Valor)
                .GreaterThan(0).WithMessage("Valor deve ser maior que zero.");

            RuleFor(x => x.Tipo)
                .NotEqual(default(TipoTransacao))
                .WithMessage("Tipo da transação é obrigatório.");

            RuleFor(x => x.Tipo)
                .IsInEnum()
                .WithMessage("Tipo da transação inválido.");

            RuleFor(x => x.CategoriaId)
                .GreaterThan(0).WithMessage("CategoriaId deve ser um número positivo.");

            RuleFor(x => x.PessoaId)
                .GreaterThan(0).WithMessage("PessoaId deve ser um número positivo.");
        }
    }
}
