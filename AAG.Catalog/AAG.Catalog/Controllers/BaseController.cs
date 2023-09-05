using AAG.Catalog.Infra.Common.Contracts;
using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace AAG.Catalog.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected ICollection<string> Erros = new List<string>();
        private readonly string[] statusComBadRequesst = { "PUT", "DELETE", };
        private readonly string get = "GET";

        [NonAction]
        protected new ActionResult Response(ICommandResult result)
        {
            if (result != null && result.Success)
            {
                return Ok(result);
            }

            return BadRequest((IReadOnlyCollection<Notification>)result?.Data);
        }

        [NonAction]

        public IActionResult CustomPostResponse(ICommandResult result)
        {
            return result.Success ? (IActionResult)Ok(result) : UnprocessableEntity(result);
        }

        [NonAction]
        public IActionResult CustomResponse(IEnumerable<object> result)
        {
            return result?.Any() ?? false ? Ok(result) : NoContent();
        }

        [NonAction]
        public IActionResult CustomResponse(object result)
        {
            return result == null ? UnprocessableEntity(result) : Ok(result);
        }

        [NonAction]

        public IActionResult CustomResponse(ICommandResult result, string redirectUrl = "")
        {
            string requestDone = Request.Method.ToUpper();

            if (!result.Success || RequestNoReturn(result))
            {
                return statusComBadRequesst.Contains(requestDone)
                      ? BadRequest(result)
                      : requestDone == get
                      ? NoContent()
                      : (IActionResult)UnprocessableEntity(result);

            }

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                return Created(redirectUrl, result);
            }

            return Ok(result);
        }

        [NonAction]
        private bool RequestNoReturn(ICommandResult result)
            => Request.Method.ToUpper() == get && (result.Data is null || result.Data == Array.Empty<object>());
    }
}
