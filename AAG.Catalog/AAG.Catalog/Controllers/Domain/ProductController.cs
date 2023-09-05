using AAG.Catalog.Domain.Commands.Input.Products;
using AAG.Catalog.Domain.Handlers;
using AAG.Catalog.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AAG.Catalog.Controllers.Domain;

[ApiVersion("1.0")]
[Route("product")]
public class ProductController : BaseController
{
    private readonly ProductHandler _productHandler;
    private readonly IProductRepository _productRepository;

    public ProductController(ProductHandler productHandler, IProductRepository productRepository)
    {
        _productHandler = productHandler;
        _productRepository = productRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return CustomResponse(new { Data = await _productRepository.Get(id) });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return CustomResponse(new { Data = await _productRepository.GetAll() });
    }

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetProductByCategory(string categoryId)
    {
        return CustomResponse(new { Data = await _productRepository.GetByCatetoryId(categoryId) });
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateProductCommand command)
    {
        var result = await _productHandler.Handle(command);
        return CustomResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(UpdateProductCommand command, string id)
    {
        var result = await _productHandler.Handle(command, id);
        return CustomResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _productHandler.Handle(id);
        return CustomResponse(result);
    }
}
