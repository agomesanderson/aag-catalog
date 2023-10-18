using AAG.Catalog.Domain.Commands.Input.Products;
using AAG.Catalog.Domain.Commands.Output.Base;
using AAG.Catalog.Domain.Commands.Output.Products;
using AAG.Catalog.Domain.Handlers;
using AAG.Catalog.Domain.Queries.Products;
using AAG.Catalog.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AAG.Catalog.Controllers.Domain;

/// <summary>
/// Produto
/// </summary>
[ApiVersion("1.0")]
[Route("product")]
public class ProductController : MainController
{
    private readonly ProductHandler _productHandler;
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Produto
    /// </summary>
    /// <param name="productHandler"></param>
    /// <param name="productRepository"></param>
    public ProductController(ProductHandler productHandler, IProductRepository productRepository)
    {
        _productHandler = productHandler;
        _productRepository = productRepository;
    }

    /// <summary>
    /// Consultar produto
    /// </summary>
    /// <param name="id">Id do produto</param>
    /// <returns>Retorna o objeto do produto</returns>
    /// <response code="200">Sucess response</response>
    /// <response code="404">Not found response</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SuccessCommandResult<ProductQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var data = ProductQueryResult.CreateProductQueryResult(await _productRepository.Get(id));

        if (data is null)
            return CustomResponse(new FailureCommandResult<ProductQueryResult>("Não há registros", 404));
        
        return CustomResponse(new SuccessCommandResult<ProductQueryResult>(data, "Consulta realizada com sucesso", 200));
    }

    /// <summary>
    /// Consulta todos os produtos
    /// </summary>
    /// <returns>Lista de objeto dos produtos</returns>
    /// <response code="200">Sucess response</response>
    /// <response code="404">Not found response</response>
    [HttpGet("all")]
    [ProducesResponseType(typeof(SuccessCommandResult<List<ProductQueryResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var data = ProductQueryResult.CreateProductQueryResult(await _productRepository.GetAll());

        if (data is null)
            return CustomResponse(new FailureCommandResult<List<ProductQueryResult>>("Não há registros", 404));
        
        return CustomResponse(new SuccessCommandResult<List<ProductQueryResult>>(data, "Consulta realizada com sucesso", 200));
    }

    /// <summary>
    /// Consulta os produtos pela categoria
    /// </summary>
    /// <param name="categoryId">Id da categoria</param>
    /// <returns>Retorna o objeto do produto</returns>
    /// <response code="200">Sucess response</response>
    /// <response code="404">Not found response</response>
    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetProductByCategory(string categoryId)
    {
        var data = ProductQueryResult.CreateProductQueryResult(await _productRepository.GetByCatetoryId(categoryId));

        if (data is null)
            return CustomResponse(new FailureCommandResult<List<ProductQueryResult>>("Não há registros", 404));
        
        return CustomResponse(new SuccessCommandResult<List<ProductQueryResult>>(data, "Consulta realizada com sucesso", 200));
    }

    /// <summary>
    /// Cria um produto
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST 
    ///     {
    ///         "CategoryId": "string"
    ///         "Name": "string"
    ///         "Description": "string"
    ///         "Price": "decimal"
    ///     }
    ///
    /// </remarks>
    /// <param name="command">Objeto para criar produto</param>
    /// <returns>Id do produto criado</returns>
    /// <response code="201">Created response</response>
    /// <response code="422">validation error</response>
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
    /// <remarks>
    /// Sample request:
    ///
    ///     POST 
    ///     {
    ///         "CategoryId": "string"
    ///         "Name": "string"
    ///         "Description": "string"
    ///         "Price": "decimal"
    ///     }
    ///
    /// </remarks>
    /// <param name="command">Objeto para atualizar produto</param>
    /// <param name="id">Id do produto</param>
    /// <returns></returns>
    /// <response code="204">No content response</response>
    /// <response code="422">validation error</response>
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
    /// <param name="id">Id do produto</param>
    /// <returns></returns>
    /// <response code="204">No content response</response>
    /// <response code="422">validation error</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(SuccessCommandResult<ProductCommandResult>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _productHandler.Handle(id);
        return CustomResponse(result);
    }
}
