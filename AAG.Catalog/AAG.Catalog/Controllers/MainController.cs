using Microsoft.AspNetCore.Mvc;

namespace AAG.Catalog.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        [NonAction]
        public IActionResult CustomResponse(dynamic result)
        {
            if (result is null)
                return NotFound(result);

            if (result.StatusCode < 0)
            {
                if (result.Success)
                    return Ok(result);

                return BadRequest(result);
            }


            if (result.Success)
            {
                return new ObjectResult(result)
                {
                    StatusCode = result.StatusCode,
                    Value = new
                    {
                        result.Success,
                        result.Message,
                        result.Data
                    }
                };
            }

            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode,
                Value = new
                {
                    result.Success,
                    result.Message,
                    result.Errors
                }
            };
        }
    }
}
