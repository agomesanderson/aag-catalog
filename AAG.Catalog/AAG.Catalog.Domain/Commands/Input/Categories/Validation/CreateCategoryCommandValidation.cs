using FluentValidation;

namespace AAG.Catalog.Domain.Commands.Input.Categories.Validation;

public class CreateCategoryCommandValidation : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome não pode ser vazio ou nulo");
    }
}
