using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    public class BaseController : ControllerBase
    {
        public IActionResult SendSuccess(object? response = null)
        {
            return Ok(response);
        }
    }
}
