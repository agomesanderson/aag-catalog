using AAG.Catalog.Domain.Commands.Input.Categories;
using AAG.Catalog.Domain.Commands.Input.Categories.Validation;
using AAG.Catalog.Domain.Commands.Output.Base;
using AAG.Catalog.Domain.Commands.Output.Categories;
using AAG.Catalog.Domain.Entities;
using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Infra.Common;
using AAG.Catalog.Infra.Common.Contracts;

namespace AAG.Catalog.Domain.Handlers;

public class CategoryHandler
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;

    public CategoryHandler(ICategoryRepository categoryRepository, IProductRepository productRepository)
    {
        _categoryRepository = categoryRepository;
        _productRepository = productRepository;
    }

    public async Task<GenericCommandResult<CategoryCommandResult>> Handle(CreateCategoryCommand command)
    {
        var validator = new CreateCategoryCommandValidation();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new ErrorItem(e.ErrorCode, e.ErrorMessage));
            return new FailureCommandResult<CategoryCommandResult>("Categoria inválida", errors);
        }

        var category = Category.Create(command);

        await _categoryRepository.Insert(category);

        return new SuccessCommandResult<CategoryCommandResult>(new CategoryCommandResult { Id = category.Id }, "Categoria criada com sucesso", 201);
    }

    public async Task<GenericCommandResult<CategoryCommandResult>> Handle(UpdateCategoryCommand command, string id)
    {
        var validator = new UpdateCategoryCommandValidation();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new ErrorItem(e.ErrorCode, e.ErrorMessage));
            return new FailureCommandResult<CategoryCommandResult>("Categoria inválida", errors);
        }

        if (id is null)
            return new FailureCommandResult<CategoryCommandResult>("Categoria inválida");

        var foundCategory = await _categoryRepository.Get(id);

        if (foundCategory is null)
            return new FailureCommandResult<CategoryCommandResult>("Categoria não localizada", 404);

        var category = Category.Update(command, id);

        await _categoryRepository.Update(category);

        return new SuccessCommandResult<CategoryCommandResult>(new CategoryCommandResult { Id = category.Id }, "Categoria alterada com sucesso", 204);
    }

    public async Task<GenericCommandResult> Handle(string id)
    {
        if (id is null)
            return new FailureCommandResult<CategoryCommandResult>("Categoria inválida");

        var foundCategory = await _categoryRepository.Get(id);

        if (foundCategory is null)
            return new FailureCommandResult<CategoryCommandResult>("Categoria não localizada", 404);

        var hasCategoryWithProducts = await _productRepository.GetByCatetoryId(id);

        if (hasCategoryWithProducts is not null && hasCategoryWithProducts.Count > 0)
            return new FailureCommandResult<CategoryCommandResult>("Já há produtos na categoria, remova os produtos para poder excluir a categoria");

        await _categoryRepository.Delete(id);

        return new SuccessCommandResult("Categoria removida com sucesso", 204);
    }
}
