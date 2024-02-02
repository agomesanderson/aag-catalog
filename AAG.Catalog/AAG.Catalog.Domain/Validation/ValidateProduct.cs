using AAG.Catalog.Domain.Commands.Input.Products;
using AAG.Catalog.Domain.Commands.Input.Products.Validation;
using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Infra.Common;

namespace AAG.Catalog.Domain.Validation;

internal sealed class ValidateProduct : Validation
{
    ICategoryRepository _categoryRepository;

    public ValidateProduct(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    internal override Response ValidationData<T>(T arg)
    {
        if (arg is CreateProductCommand)
        {
            CreateProductCommand input = (CreateProductCommand)(object)arg;

            var validatorDomain = new CreateProductCommandDomainValidation(_categoryRepository);
            var validationResultDomain = validatorDomain.Validate(input);

            if (!validationResultDomain.IsValid)
                return new Response(false,
                                    validationResultDomain.Errors.Select(e => 
                                        new ErrorItem(e.PropertyName, e.ErrorMessage)).ToString());

            return new Response(true);
        }

        return new Response(false, "Tipo incorreto");
    }

    internal override Response ValidationInput<T>(T arg)
    {
        if (arg is CreateProductCommand)
        {
            CreateProductCommand input = (CreateProductCommand)(object)arg;

            var validator = new CreateProductCommandValidation();
            var validationResult = validator.Validate(input);

            if (!validationResult.IsValid)
                return new Response(false,
                                    validationResult.Errors.Select(e =>
                                        new ErrorItem(e.PropertyName, e.ErrorMessage)).ToString());

            return new Response(true);
        }

        return new Response(false, "Tipo incorreto");
    }
}
