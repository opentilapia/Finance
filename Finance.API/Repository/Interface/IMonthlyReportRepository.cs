using Finance.API.Domain.Class;

namespace Finance.API.Repository.Interface
{
    public interface IMonthlyReportRepository
    {
        Task<List<MonthlyReport>> GetPaginated(int pageSize, DateTime lastMonthEntry);
        Task<bool> Insert(MonthlyReport entity);
        Task<MonthlyReport> GetById(string id);
        Task<bool> UpdateByComputation(MonthlyReport entity);
        Task<bool> UpdateBasicDetails(string id, decimal totalFunds, string remarks);
    }
}
