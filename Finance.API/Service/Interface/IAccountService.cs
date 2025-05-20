using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;

namespace Finance.API.Service.Interface
{
    public interface IAccountService
    {
        Task<bool> Upsert(UpsertAccountRequestVM entity);
        Task<AccountVM> GetById(string id);
        Task<List<AccountVM>> GetAll();
        Task<AccountOverviewVM> GetOverview();
        Task<bool> Delete(string id);
    }
}
