using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public IActionResult SendSuccess(object? response = null)
        {
            if (response is null or (object)true)
            {
                return Ok(new { message = "Success" });
            }

            return Ok(new 
            { 
                message = "Success",
                data = response
            });
        }

        public IActionResult SendError(Exception e)
        {
            var errorResponse = new
            {
                message = "An error has occured",
                details = e.Message,
                stack = e.StackTrace
            };
            return StatusCode(500, errorResponse);
        }

    }
}
