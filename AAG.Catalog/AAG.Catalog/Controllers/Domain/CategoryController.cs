using AAG.Catalog.Domain.Commands.Input.Categories;
using AAG.Catalog.Domain.Handlers;
using AAG.Catalog.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AAG.Catalog.Controllers.Domain;

[ApiVersion("1.0")]
[Route("category")]
public class CategoryController : BaseController
{
    private readonly CategoryHandler _categoryHandler;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(CategoryHandler categoryHandler, ICategoryRepository categoryRepository)
    {
        _categoryHandler = categoryHandler;
        _categoryRepository = categoryRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        return CustomResponse(new { Data = await _categoryRepository.Get(id) });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return CustomResponse(new { Data = await _categoryRepository.GetAll() });
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCategoryCommand command)
    {
        var result = await _categoryHandler.Handle(command);
        return CustomResponse(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(UpdateCategoryCommand command, string id)
    {
        var result = await _categoryHandler.Handle(command, id);
        return CustomResponse(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _categoryHandler.Handle(id);
        return CustomResponse(result);
    }
}
