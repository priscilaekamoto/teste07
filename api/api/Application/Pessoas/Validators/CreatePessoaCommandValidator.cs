using FluentValidation;
using api.Application.Pessoas.Commands;

namespace api.Application.Pessoas.Validators
{
    public class CreatePessoaCommandValidator : AbstractValidator<CreatePessoaCommand>
    {
        public CreatePessoaCommandValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Idade)
                .InclusiveBetween(0, 120).WithMessage("Idade deve ser entre 0 e 120.");
        }
    }
}
