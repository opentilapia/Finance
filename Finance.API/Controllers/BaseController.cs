using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public IActionResult SendSuccess(object? response = null)
        {
            return Ok(response);
        }

        public IActionResult SendError(Exception e)
        {
            var errorResponse = new
            {
                message = "An error has occured",
                details = e.Message
            };
            return StatusCode(500, errorResponse);
        }

    }
}
