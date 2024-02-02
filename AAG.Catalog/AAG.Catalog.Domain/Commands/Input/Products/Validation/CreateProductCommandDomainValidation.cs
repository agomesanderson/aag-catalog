using AAG.Catalog.Domain.Repositories;
using FluentValidation;

namespace AAG.Catalog.Domain.Commands.Input.Products.Validation;

public class CreateProductCommandDomainValidation : AbstractValidator<CreateProductCommand>
{
    private ICategoryRepository _categoryRepository;

    public CreateProductCommandDomainValidation(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;

        RuleFor(x => x.CategoryId)
            .Must(ValidateCategory).WithMessage("Id da categoria precisa existir no banco");
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
}