using api.Application.Categorias.Commands;
using api.Models.Enums;
using FluentValidation;

namespace api.Application.Categorias.Validators
{
    public class CreateCategoriaCommandValidator : AbstractValidator<CreateCategoriaCommand>
    {
        public CreateCategoriaCommandValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória.")
                .MaximumLength(500);

            RuleFor(x => x.Finalidade)
                .NotEqual(default(Finalidade))
                .WithMessage("Finalidade é obrigatória.")
                .IsInEnum();
        }
    }
}
