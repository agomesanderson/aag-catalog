using AAG.Catalog.Domain.Commands.Input.Products;
using AAG.Catalog.Domain.Commands.Output.Base;
using AAG.Catalog.Domain.Commands.Output.Products;
using AAG.Catalog.Domain.Handlers;
using AAG.Catalog.Domain.Queries.Categories;
using AAG.Catalog.Domain.Queries.Products;
using AAG.Catalog.Domain.Repositories;
using AAG.Catalog.Infra.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AAG.Catalog.Controllers.Domain;

[ApiVersion("1.0")]
[Route("product")]
public class ProductController : MainController
{
    private readonly ProductHandler _productHandler;
    private readonly IProductRepository _productRepository;

    public ProductController(ProductHandler productHandler, IProductRepository productRepository)
    {
        _productHandler = productHandler;
        _productRepository = productRepository;
    }

    /// <summary>
    /// Consultar produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna o objeto do produto</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SuccessCommandResult<ProductQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var data = ProductQueryResult.CreateProductQueryResult(await _productRepository.Get(id));

        if (data is null)
            return CustomResponse(new FailureCommandResult<ProductQueryResult>("Não há registros", 404));
        else
            return CustomResponse(new SuccessCommandResult<ProductQueryResult>(data, "Consulta realizada com sucesso", 200));
    }

    /// <summary>
    /// Consulta todos os produtos
    /// </summary>
    /// <returns>Lista de objeto dos produtos</returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(SuccessCommandResult<List<ProductQueryResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var data = ProductQueryResult.CreateProductQueryResult(await _productRepository.GetAll());

        if (data is null)
            return CustomResponse(new FailureCommandResult<List<ProductQueryResult>>("Não há registros", 404));
        else
            return CustomResponse(new SuccessCommandResult<List<ProductQueryResult>>(data, "Consulta realizada com sucesso", 200));
    }

    /// <summary>
    /// Consulta os produtos pela categoria
    /// </summary>
    /// <param name="categoryId"></param>
    /// <returns>Retorna o objeto do produto</returns>
    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetProductByCategory(string categoryId)
    {
        var data = ProductQueryResult.CreateProductQueryResult(await _productRepository.GetByCatetoryId(categoryId));

        if (data is null)
            return CustomResponse(new FailureCommandResult<List<ProductQueryResult>>("Não há registros", 404));
        else
            return CustomResponse(new SuccessCommandResult<List<ProductQueryResult>>(data, "Consulta realizada com sucesso", 200));
    }

    /// <summary>
    /// Cria um produto
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Id do produto criado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SuccessCommandResult<ProductCommandResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Post(CreateProductCommand command)
    {
        var result = await _productHandler.Handle(command);
        return CustomResponse(result);
    }

    /// <summary>
    /// Atualiza um produto
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SuccessCommandResult<ProductCommandResult>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Put(UpdateProductCommand command, string id)
    {
        var result = await _productHandler.Handle(command, id);
        return CustomResponse(result);
    }

    /// <summary>
    /// Remove um produto
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(SuccessCommandResult<ProductCommandResult>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _productHandler.Handle(id);
        return CustomResponse(result);
    }
}
