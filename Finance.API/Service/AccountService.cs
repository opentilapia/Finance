using Finance.API.Common.Constant;
using Finance.API.DataService.Interface;
using Finance.API.Domain.Class;
using Finance.API.Domain.Enum;
using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;
using Finance.API.Service.Interface;

namespace Finance.API.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;
        private readonly IUserProfileRepository _userProfileRepository;

        public AccountService(IAccountRepository repo, IUserProfileRepository userProfileRepository)
        {
            _repo = repo;
            _userProfileRepository = userProfileRepository;
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

        public async Task<AccountOverviewVM> GetOverview()
        {
            UserProfile userProfile = await _userProfileRepository
                .GetById(AppConstant.USER_PROFILE_PERSISTENT_ID);

            if (userProfile == null)
            {
                throw new ApplicationException("No user profile found.");
            }

            List<Account> sources = await _repo.GetAll();
            List<AccountVM> accountVMList = new List<AccountVM>();

            AccountOverviewVM result = new AccountOverviewVM();

            foreach (Account entity in sources)
            {
                switch (entity.Type)
                {
                    case AccountTypeEnum.Payables:
                        result.TotalPayables += entity.Amount;
                        break;
                    case AccountTypeEnum.Receivables:
                        result.TotalReceivables += entity.Amount;
                        break;
                    case AccountTypeEnum.Cash:
                    case AccountTypeEnum.HysaDeposit:
                    case AccountTypeEnum.BankDeposit:
                        result.TotalFlexibleMoney += entity.Amount;
                        result.TotalCash += entity.Amount;
                        break;
                    case AccountTypeEnum.TimeDeposit:
                        result.TotalLongTermMoney += entity.Amount;
                        result.TotalFixedIncome += entity.Amount;
                        break;
                    case AccountTypeEnum.Equities:
                        result.TotalLongTermMoney += entity.Amount;
                        result.TotalEquities += entity.Amount;
                        break;
                    case AccountTypeEnum.CreditCardRemaining:
                        if(userProfile.CreditLimit > 0)
                            result.TotalPayables += userProfile.CreditLimit - entity.Amount;
                        break;
                    default:
                        break;
                }

                accountVMList.Add(new AccountVM(entity));
            }
            result.Accounts = accountVMList;

            return result;
        }

        public async Task<bool> Delete(string id)
        {
            return await _repo.Delete(id);
        }
    }
}
