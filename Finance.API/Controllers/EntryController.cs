using Finance.API.Domain.ViewModel.Request;
using Finance.API.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntryController : BaseController
    {
        private readonly IEntryService _service;
        private readonly ICategoryService _categoryService;

        public EntryController(IEntryService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Upsert([FromBody] UpsertEntryRequestVM request)
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

        [HttpGet("Paginate")]
        public async Task<IActionResult> GetPaginated(string categoryId, DateTime? lastEntryDate)
        {
            try
            {
                var result = await _service.GetPaginated(categoryId, lastEntryDate);
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


        [HttpPost("Import")]
        public async Task<IActionResult> Import([FromForm] string categoryId, [FromForm] IFormFile file)
        {
            try
            {
                var result = await _service.Import(categoryId, file);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }
    }
}
