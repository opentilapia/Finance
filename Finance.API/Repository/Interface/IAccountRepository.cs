using Finance.API.Domain.Class;

namespace Finance.API.DataService.Interface
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAll();
        Task<Account> GetById(string id);
        Task<bool> Upsert(Account entity);
        Task<bool> Delete(string id);
    }
}
