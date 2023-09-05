using AAG.Catalog.Domain.Commands.Input.Categories;
using AAG.Catalog.Domain.Commands.Output;
using AAG.Catalog.Domain.Entities;
using AAG.Catalog.Domain.Repositories;
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

    public async Task<ICommandResult> Handle(CreateCategoryCommand command)
    {
        command.Validate();

        if (!command.IsValid)
            return new GenericCommandResult(false, "Categoria inválida", command.Notifications);

        var category = Category.Create(command);

        await _categoryRepository.Insert(category);

        return new GenericCommandResult(true, "Categoria criada com sucesso", new { Id = category.Id });
    }

    public async Task<ICommandResult> Handle(UpdateCategoryCommand command, string id)
    {
        command.Validate();

        if (!command.IsValid)
            return new GenericCommandResult(false, "Categoria inválida", command.Notifications);

        if (id is null)
            return new GenericCommandResult(false, "Categoria inválida", null!);

        var foundCategory = await _categoryRepository.Get(id);

        if (foundCategory is null)
            return new GenericCommandResult(false, "Categoria não localizada", null!);

        var category = Category.Update(command, id);

        await _categoryRepository.Update(category);

        return new GenericCommandResult(true, "Categoria alterada com sucesso", new { Id = category.Id });
    }

    public async Task<ICommandResult> Handle(string id)
    {
        if (id is null)
            return new GenericCommandResult(false, "Categoria inválida", null!);

        var foundCategory = await _categoryRepository.Get(id);

        if (foundCategory is null)
            return new GenericCommandResult(false, "Categoria não localizada", null!);

        var hasCategoryWithProducts = await _productRepository.GetByCatetoryId(id);

        if (hasCategoryWithProducts is not null && hasCategoryWithProducts.Count > 0)
            return new GenericCommandResult(false, "Já há produtos na categoria, remova os produtos para poder excluir a categoria", null!);

        await _categoryRepository.Delete(id);

        return new GenericCommandResult(true, "Categoria removida com sucesso", null!);
    }
}
