using Finance.API.Model;
using Finance.API.Service.Interface;
using Finance.API.ViewModel;
using Finance.API.ViewModel.Request;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntryController : BaseController
    {
        private readonly IEntryService _service;

        public EntryController(IEntryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateEntry([FromBody] UpsertEntryRequestVM request)
        {
            bool result = await _service.Upsert(request);
            return SendSuccess(result);
        }

        [HttpGet("paginate")]
        public async Task<IActionResult> GetByPagination(DateTime? lastEntryDate)
        {
            var result = await _service.GetPaginated(lastEntryDate);
            return SendSuccess(result);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteById(string id)
        {
            var result = await _service.Delete(id);
            return SendSuccess(result);
        }

        [HttpDelete]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _service.GetById(id);
            return SendSuccess(result);
        }
    }
}
