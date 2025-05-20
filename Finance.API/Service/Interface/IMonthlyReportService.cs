using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;


namespace Finance.API.Service.Interface
{
    public interface IMonthlyReportService
    {
        Task<List<MonthlyReportVM>> GetPaginated(DateTime? lastMonthEntry);
        Task<bool> Compute(DateTime? month);
        Task<bool> RecomputeById(string id);
        Task<bool> UpdateBasicDetails(UpdateMonthlyReportBasicDetailsRequestVM request);
    }
}
