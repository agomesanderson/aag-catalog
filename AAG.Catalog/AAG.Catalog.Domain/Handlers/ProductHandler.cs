using AAG.Catalog.Domain.Commands.Input.Products;
using AAG.Catalog.Domain.Commands.Input.Products.Validation;
using AAG.Catalog.Domain.Commands.Output.Base;
using AAG.Catalog.Domain.Commands.Output.Categories;
using AAG.Catalog.Domain.Commands.Output.Products;
using AAG.Catalog.Domain.Entities;
using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Domain.Validation;
using AAG.Catalog.Infra.Common;

namespace AAG.Catalog.Domain.Handlers;

public class ProductHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public ProductHandler(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task<GenericResult> Handle(CreateProductCommand command)
    {
        //var validator = new CreateProductCommandValidation();
        //var validationResult = validator.Validate(command);

        //if (!validationResult.IsValid)
        //{
        //    var errors = validationResult.Errors.Select(e => new ErrorItem(e.ErrorCode, e.ErrorMessage));
        //    return new FailureCommandResult<ProductCommandResult>(errors, "Produto inválido");
        //}

        //var foundCategory = await _categoryRepository.Get(command.CategoryId);

        //if (foundCategory is null)
        //    return new FailureCommandResult<ProductCommandResult>("Cagoria não localizada");

        //*****
        // PROPOSTA
        var newValidator = new ValidateProduct(_categoryRepository);

        var resultCommand = newValidator.ValidationInput(command);
        if (resultCommand.Success)
        {
            var resultData = newValidator.ValidationData(command);
            if (!resultData.Success)
                return new GenericResult(422, resultCommand.Message);            
        }
        else
            return new GenericResult(422, resultCommand.Message);

        var product = Product.Create(command);

        await _productRepository.Insert(product);

        return new GenericResult(201);

        //*****

        //var validator = CreateProductCommandValidate.Validate(command, _categoryRepository); //command.Validate(_categoryRepository);

        //if (!validator.IsValid)
        //    return new FailureCommandResult<ProductCommandResult>(validator.Errors!, "Produto inválido");

        //var product = Product.Create(command);

        //await _productRepository.Insert(product);

        //return new SuccessCommandResult<ProductCommandResult>(new ProductCommandResult { Id = product.Id }, "Produto criado com sucesso", 201);
    }

    public async Task<GenericCommandResult<ProductCommandResult>> Handle(UpdateProductCommand command, string id)
    {
        var validator = new UpdateProductCommandValidation();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new ErrorItem(e.ErrorCode, e.ErrorMessage));
            return new FailureCommandResult<ProductCommandResult>(errors, "Produto inválido");
        }

        if (id is null)
            return new FailureCommandResult<ProductCommandResult>("Produto inválida");

        var foundCategory = await _categoryRepository.Get(command.CategoryId);

        if (foundCategory is null)
            return new FailureCommandResult<ProductCommandResult>("Cagoria não localizada", 404);

        var foundProduct = await _productRepository.Get(id);

        if (foundProduct is null)
            return new FailureCommandResult<ProductCommandResult>("Cagoria não localizada", 404);

        var product = Product.Update(command, id);

        await _productRepository.Update(product);

        return new SuccessCommandResult<ProductCommandResult>(new ProductCommandResult { Id = product.Id }, "Produto alterado com sucesso", 204);
    }

    public async Task<GenericCommandResult> Handle(string id)
    {
        if (id is null)
            return new FailureCommandResult<CategoryCommandResult>("Produto inválida");

        var foundProduct = await _productRepository.Get(id);

        if (foundProduct is null)
            return new FailureCommandResult<CategoryCommandResult>("Produto não localizado", 404);

        await _productRepository.Delete(id);

        return new SuccessCommandResult("Produto removido com sucesso", 204);
    }
}
