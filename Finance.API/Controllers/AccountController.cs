using Finance.API.Domain.ViewModel.Request;
using Finance.API.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody] UpsertAccountRequestVM request)
        {
            try
            {
                bool result = await _service.Upsert(request);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAll();
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        [HttpGet("Overview")]
        public async Task<IActionResult> GetOverview()
        {
            try
            {
                var result = await _service.GetOverview();
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
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

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _service.Delete(id);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }
    }
}
