using Finance.API.Domain.ViewModel.Request;
using Finance.API.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonthlyReportController : BaseController
    {
        private readonly IMonthlyReportService _service;

        public MonthlyReportController(IMonthlyReportService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasicDetails([FromBody] UpdateMonthlyReportBasicDetailsRequestVM request)
        {
            try
            {
                bool result = await _service.UpdateBasicDetails(request);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        [HttpGet("Paginate")]
        public async Task<IActionResult> GetPaginated(DateTime? lastEntryDate)
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

        [HttpPost("Compute")]
        public async Task<IActionResult> Compute()
        {
            try
            {
                
                var result = await _service.Compute();
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        [HttpPost("{id}/Recompute")]
        public async Task<IActionResult> Recompute(string id)
        {
            try
            {
                var result = await _service.Recompute(id);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }
    }
}
