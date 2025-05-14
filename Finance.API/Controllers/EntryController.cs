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
        readonly IEntryService _service;
        readonly ICategoryService _categoryService;

        public EntryController(IEntryService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateEntry([FromBody] UpsertEntryRequestVM request)
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

        [HttpGet("paginate")]
        public async Task<IActionResult> GetByPagination(DateTime? lastEntryDate)
        {
            try
            {
                var result = await _service.GetPaginated(lastEntryDate);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteById(string id)
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

        [HttpDelete]
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
    }
}
