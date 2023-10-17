using AAG.Catalog.Domain.Commands.Input.Categories;
using AAG.Catalog.Domain.Commands.Output.Base;
using AAG.Catalog.Domain.Commands.Output.Categories;
using AAG.Catalog.Domain.Handlers;
using AAG.Catalog.Domain.Queries.Categories;
using AAG.Catalog.Domain.Queries.Products;
using AAG.Catalog.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AAG.Catalog.Controllers.Domain;

[ApiVersion("1.0")]
[Route("category")]
public class CategoryController : MainController
{
    private readonly CategoryHandler _categoryHandler;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(CategoryHandler categoryHandler, ICategoryRepository categoryRepository)
    {
        _categoryHandler = categoryHandler;
        _categoryRepository = categoryRepository;
    }

    /// <summary>
    /// Consulta uma categoria
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna o objeto da categoria</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SuccessCommandResult<CategoryQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var data = CategoryQueryResult.CreateCategoryQueryResult(await _categoryRepository.Get(id));

        if (data is null)
            return CustomResponse(new FailureCommandResult<CategoryQueryResult>("Não há registros", 404));
        else
            return CustomResponse(new SuccessCommandResult<CategoryQueryResult>(data, "Consulta realizada com sucesso", 200));
    }

    /// <summary>
    /// Consulta todas as categorias
    /// </summary>
    /// <returns>Lista de objetod das categorias</returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(SuccessCommandResult<List<CategoryQueryResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var data = CategoryQueryResult.CreateCategoryQueryResult(await _categoryRepository.GetAll());

        if (data is null)
            return CustomResponse(new FailureCommandResult<List<CategoryQueryResult>>("Não há registros", 404));
        else
            return CustomResponse(new SuccessCommandResult<List<CategoryQueryResult>>(data, "Consulta realizada com sucesso", 200));
    }

    /// <summary>
    /// Cria uma categoria
    /// </summary>
    /// <param name="command"></param>
    /// <returns>Id da categoria criado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SuccessCommandResult<CategoryCommandResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Post(CreateCategoryCommand command)
    {
        var result = await _categoryHandler.Handle(command);
        return CustomResponse(result);
    }

    /// <summary>
    /// Atualiza uma categoria
    /// </summary>
    /// <param name="command"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SuccessCommandResult<CategoryCommandResult>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Put(UpdateCategoryCommand command, string id)
    {
        var result = await _categoryHandler.Handle(command, id);
        return CustomResponse(result);
    }

    /// <summary>
    /// Remove uma categoria
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(SuccessCommandResult<CategoryCommandResult>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(FailureCommandResult), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _categoryHandler.Handle(id);
        return CustomResponse(result);
    }
}
