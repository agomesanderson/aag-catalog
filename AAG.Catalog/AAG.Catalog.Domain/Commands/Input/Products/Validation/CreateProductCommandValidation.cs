using FluentValidation;

namespace AAG.Catalog.Domain.Commands.Input.Products.Validation;

public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidation()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Id da categoria, não pode ser vazio, nulo ou invalida");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome não pode ser vazio ou nulo");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Descrição não pode ser vazio ou nulo");

        RuleFor(x => x.Price)
            .Must(x => x > 0).WithMessage("Preço não pode zero");
    }
}