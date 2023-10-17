using AAG.Catalog.Infra.Common;
using Microsoft.AspNetCore.Mvc;

namespace AAG.Catalog.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        [NonAction]
        public IActionResult CustomResponse(GenericCommandResult result)
        {
            if (result is null)
            {
                return NotFound(result);
            }

            if (result.StatusCode < 0)
            {
                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return new ObjectResult(result)
            {
                StatusCode = result.StatusCode,
                Value = result
            };
        }
    }
}
