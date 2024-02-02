using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Infra.Common;

namespace AAG.Catalog.Domain.Commands.Input.Products.Validation
{
    public class CreateProductCommandValidate
    {
        public static (bool IsValid, IEnumerable<ErrorItem>? Errors) Validate(CreateProductCommand command, ICategoryRepository categoryRepository)
        {
            var validator = new CreateProductCommandValidation();
            var validationResult = validator.Validate(command);

            var validatorDomain = new CreateProductCommandDomainValidation(categoryRepository);
            var validationResultDomain = validatorDomain.Validate(command);

            if (!validationResult.IsValid || !validationResultDomain.IsValid)
                return (validationResult.IsValid && validationResultDomain.IsValid,
                        validationResult.Errors.Select(e => new ErrorItem(e.PropertyName, e.ErrorMessage))
                        .Concat(validationResultDomain.Errors.Select(e => new ErrorItem(e.PropertyName, e.ErrorMessage))));

            return (validationResult.IsValid && validationResultDomain.IsValid, null);
        }
    }
}
