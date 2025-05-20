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
        private readonly IUserProfileService _userProfileService;

        public MonthlyReportController(IMonthlyReportService service, IUserProfileService userProfileService)
        {
            _service = service;
            _userProfileService = userProfileService;
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
        public async Task<IActionResult> Compute([FromQuery] DateTime? month)
        {
            try
            {
                
                var result = await _service.Compute(month);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }

        [HttpPost("RecomputeById/{id}")]
        public async Task<IActionResult> RecomputeById(string id)
        {
            try
            {
                var result = await _service.RecomputeById(id);
                return SendSuccess(result);
            }
            catch (Exception e)
            {
                return SendError(e);
            }
        }
    }
}
