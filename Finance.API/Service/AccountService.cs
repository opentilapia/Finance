using Finance.API.DataService.Interface;
using Finance.API.Domain.Class;
using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;
using Finance.API.Service.Interface;

namespace Finance.API.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Upsert(UpsertAccountRequestVM request)
        {
            Account entity = new Account(request);
            return await _repo.Upsert(entity);
        }

        public async Task<AccountVM> GetById(string id)
        {
            Account entity = await _repo.GetById(id);

            if (entity == null)
            {
                throw new ApplicationException("Not found");
            }

            return new AccountVM(entity);
        }

        public async Task<List<AccountVM>> GetAll()
        {
            List<Account> sources = await _repo.GetAll();
            List<AccountVM> result = new List<AccountVM>();
            
            foreach (Account entity in sources)
            {
                result.Add(new AccountVM(entity));
            }

            return result;
        }

        public async Task<bool> Delete(string id)
        {
            return await _repo.Delete(id);
        }
    }
}
