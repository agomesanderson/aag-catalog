using AAG.Catalog.Domain.Entities;
using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Infra.Common;
using FluentValidation;

namespace AAG.Catalog.Domain.Commands.Input.Products.Validation;

public class CreateProductCommandValidation : AbstractValidator<CreateProductCommand>
{
    private ICategoryRepository _categoryRepository;

    public CreateProductCommandValidation(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .Must(ValidateCategory).WithMessage("Id da categoria precisa existir no banco, não pode ser vazio, nulo ou invalida");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome não pode ser vazio ou nulo");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Descrição não pode ser vazio ou nulo");

        RuleFor(x => x.Price)
            .Must(x => x > 0).WithMessage("Preço não pode zero");
    }

    private bool ValidateCategory(string categoryId)
    {
        if (string.IsNullOrEmpty(categoryId))
            return false;

        var category = _categoryRepository.Get(categoryId).GetAwaiter().GetResult();

        if (category is null)
            return false;

        return true;
    }

    public static (bool IsValid, IEnumerable<ErrorItem>? Errors) Validate(CreateProductCommand command, ICategoryRepository categoryRepository)
    {
        var validator = new CreateProductCommandValidation(categoryRepository);
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
            return (validationResult.IsValid, validationResult.Errors.Select(e => new ErrorItem(e.PropertyName, e.ErrorMessage)));

        return (validationResult.IsValid, null);
    }
}