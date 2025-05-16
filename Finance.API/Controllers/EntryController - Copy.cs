//using Finance.API.Domain.ViewModel.Request;
//using Finance.API.Service.Interface;
//using Microsoft.AspNetCore.Mvc;

//namespace Finance.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class MonthlyReportController : BaseController
//    {
//        private readonly IEntryService _service;
//        private readonly ICategoryService _categoryService;

//        public MonthlyReport(IMonthlyReportService  IEntryService service, ICategoryService categoryService)
//        {
//            _service = service;
//            _categoryService = categoryService;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Upsert([FromBody] UpsertEntryRequestVM request)
//        {
//            try
//            {
//                bool result = await _service.Upsert(request);
//                return SendSuccess(result);
//            }
//            catch (Exception e)
//            {
//                return SendError(e);
//            }
//        }

//        [HttpGet("paginate")]
//        public async Task<IActionResult> GetPaginated(DateTime? lastEntryDate)
//        {
//            try
//            {
//                var result = await _service.GetPaginated(lastEntryDate);
//                return SendSuccess(result);
//            }
//            catch (Exception e)
//            {
//                return SendError(e);
//            }
//        }

//        [HttpGet]
//        public async Task<IActionResult> Delete(string id)
//        {
//            try
//            {
//                var result = await _service.Delete(id);
//                return SendSuccess(result);
//            }
//            catch (Exception e)
//            {
//                return SendError(e);
//            }
//        }

//        [HttpDelete]
//        public async Task<IActionResult> GetById(string id)
//        {
//            try
//            {
//                var result = await _service.GetById(id);
//                return SendSuccess(result);
//            }
//            catch (Exception e)
//            {
//                return SendError(e);
//            }
//        }
//    }
//}
