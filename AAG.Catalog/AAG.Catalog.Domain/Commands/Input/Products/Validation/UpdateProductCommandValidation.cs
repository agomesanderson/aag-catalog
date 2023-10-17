using FluentValidation;

namespace AAG.Catalog.Domain.Commands.Input.Products.Validation;

public class UpdateProductCommandValidation : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidation()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Id da categoria não pode ser vazio ou nulo");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome não pode ser vazio ou nulo");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Descrição não pode ser vazio ou nulo");

        RuleFor(x => x.Price)
            .NotEmpty()
            .Must(x => x > 0).WithMessage("Preço não pode zero");
    }
}
