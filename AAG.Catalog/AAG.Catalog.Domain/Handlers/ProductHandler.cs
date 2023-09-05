using AAG.Catalog.Domain.Commands.Input.Products;
using AAG.Catalog.Domain.Commands.Output;
using AAG.Catalog.Domain.Entities;
using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Infra.Common.Contracts;

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

    public async Task<ICommandResult> Handle(CreateProductCommand command)
    {
        command.Validate();

        if (!command.IsValid)
            return new GenericCommandResult(false, "Produto inválida", command.Notifications);

        var foundCategory = await _categoryRepository.Get(command.CategoryId);

        if (foundCategory is null)
            return new GenericCommandResult(false, "Cagoria não localizada", null!);

        var product = Product.Create(command);

        await _productRepository.Insert(product);

        return new GenericCommandResult(true, "Produto criado com sucesso", new { Id = product.Id });
    }

    public async Task<ICommandResult> Handle(UpdateProductCommand command, string id)
    {
        command.Validate();

        if (!command.IsValid)
            return new GenericCommandResult(false, "Produto inválida", command.Notifications);

        if (id is null)
            return new GenericCommandResult(false, "Produto inválida", null!);

        var foundCategory = await _categoryRepository.Get(command.CategoryId);

        if (foundCategory is null)
            return new GenericCommandResult(false, "Cagoria não localizada", null!);

        var foundProduct = await _productRepository.Get(id);

        if (foundProduct is null)
            return new GenericCommandResult(false, "Cagoria não localizada", null!);

        var product = Product.Update(command, id);

        await _productRepository.Update(product);

        return new GenericCommandResult(true, "Produto alterado com sucesso", new { Id = product.Id });
    }

    public async Task<ICommandResult> Handle(string id)
    {
        if (id is null)
            return new GenericCommandResult(false, "Produto inválida", null!);

        var foundProduct = await _productRepository.Get(id);

        if (foundProduct is null)
            return new GenericCommandResult(false, "Produto não localizada", null!);

        await _productRepository.Delete(id);

        return new GenericCommandResult(true, "Produto removido com sucesso", null!);
    }
}
