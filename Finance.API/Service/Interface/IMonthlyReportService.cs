using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;


namespace Finance.API.Service.Interface
{
    public interface IMonthlyReportService
    {
        public Task<List<MonthlyReportVM>> GetPaginated(int pageSize, DateTime? lastMonthEntry);
        public Task<bool> Compute(string id);
        public Task<bool> UpdateBasicDetails(UpdateMonthlyReportBasicDetailsRequestVM request);
    }
}
