using Finance.API.Domain.ViewModel.Request;
using Finance.API.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : BaseController
    {
        private readonly IUserProfileService _service;

        public UserProfileController(IUserProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _service.GetById(id);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody] UpsertUserProfileRequestVM request)
        {
            try
            {
                var result = await _service.Upsert(request);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }
    }
}
