using Finance.API.Common;
using Finance.API.Common.Constant;
using Finance.API.DataService.Interface;
using Finance.API.Domain.Class;
using Finance.API.Domain.Enum;
using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;
using Finance.API.Repository.Interface;
using Finance.API.Service.Interface;
using static Finance.API.Domain.ViewModel.MonthlyReportVM;

namespace Finance.API.Service
{
    public class MonthlyReportService : IMonthlyReportService
    {
        private readonly IMonthlyReportRepository _monthlyReportRepo;
        private readonly IEntryRepository _entryRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IUserProfileRepository _userProfileRepo;

        const int PAGE_SIZE = 10;

        public MonthlyReportService(
            IMonthlyReportRepository repo, 
            IEntryRepository entryRepo, 
            ICategoryRepository categoryRepo,
            IUserProfileRepository userProfileRepo)
        {
            _monthlyReportRepo = repo;
            _entryRepo = entryRepo;
            _categoryRepo = categoryRepo;
            _userProfileRepo = userProfileRepo;
        }

        public async Task<List<MonthlyReportVM>> GetPaginated(DateTime? lastMonthEntry)
        {
            if (!lastMonthEntry.HasValue)
                lastMonthEntry = DateTime.MaxValue;

            List<MonthlyReport> sources = await _monthlyReportRepo.GetPaginated(PAGE_SIZE, lastMonthEntry.Value);
            List<MonthlyReportVM> result = new List<MonthlyReportVM>();

            List<Category> categoryList = await _categoryRepo.GetAll();
            Dictionary<string, string> categoryDict = categoryList.ToDictionary(s => s.Id, s => s.CategoryName);

            foreach (MonthlyReport entity in sources)
            {
                MonthlyReportVM vm = new MonthlyReportVM(entity);
                vm.MonthlyCategoryEntries = new List<MonthlyCategoryEntryVM>();

                foreach (MonthlyReport.MonthlyCategoryEntry totals in entity.MonthlyCategoryEntries)
                {
                    if (!categoryDict.TryGetValue(totals.CategoryId, out string? categoryName))
                        categoryName = "DELETED CATEGORY";

                    vm.MonthlyCategoryEntries.Add(new MonthlyCategoryEntryVM(totals.CategoryId, categoryName, totals.Total));

                }

                result.Add(vm);
            }

            return result;
        }

        public async Task<bool> Compute(DateTime? month)
        {
            UserProfile userProfile = await _userProfileRepo.GetById(AppConstant.USER_PROFILE_PERSISTENT_ID);

            if (userProfile == null)
            {
                throw new ApplicationException("User profile not found.");
            }
            else if (userProfile.CurrentNetSalary == 0)
            {
                throw new ApplicationException("Net salary of user has not been setup yet.");
            }

            if (!month.HasValue)
            {
                month = DateHelper.GetDateTimePH();
            }

            month = month.Value.GetStartOfMonth();

            MonthlyReport entity = await _monthlyReportRepo.GetByMonth(month.Value);

            bool isNew = false;

            if (entity == null)
            {
                isNew = true;
                entity = new MonthlyReport();
                entity.MonthlyCategoryEntries = new List<MonthlyReport.MonthlyCategoryEntry>();
                entity.Month = month.Value;
            }

            entity = await compute(entity, userProfile);

            return isNew 
                ? await _monthlyReportRepo.Insert(entity)
                : await _monthlyReportRepo.UpdateByComputation(entity);
        }

        public async Task<bool> RecomputeById(string id)
        {
            UserProfile userProfile = await _userProfileRepo.GetById(AppConstant.USER_PROFILE_PERSISTENT_ID);

            if (userProfile == null)
            {
                throw new ApplicationException("User profile not found.");
            }
            else if(userProfile.CurrentNetSalary == 0)
            {
                throw new ApplicationException("Net salary of user has not been setup yet.");
            }

            MonthlyReport entity = await _monthlyReportRepo.GetById(id);

            if (entity == null)
            {
                throw new ApplicationException("Monthly report not found.");
            }

            entity = await compute(entity, userProfile);

            return await _monthlyReportRepo.UpdateByComputation(entity);
        }

        private async Task<MonthlyReport> compute(MonthlyReport entity, UserProfile userProfile)
        {
            List<Entry> entryList = await _entryRepo
                .GetByDateRange(entity.Month.GetStartOfMonth(), entity.Month.GetEndOfMonth());

            Dictionary<string, decimal> aggregatedCategoryAmountDict = new Dictionary<string, decimal>();

            foreach (Entry entry in entryList)
            {
                if (!aggregatedCategoryAmountDict.TryGetValue(entry.CategoryId, out decimal amount))
                {
                    aggregatedCategoryAmountDict.Add(entry.CategoryId, entry.Amount);
                    continue;
                }

                aggregatedCategoryAmountDict[entry.CategoryId] = amount + entry.Amount;
            }

            List<Category> categoryList = await _categoryRepo.GetAll();
            Dictionary<string, Category> categoryDict = categoryList.ToDictionary(s => s.Id, s => s);

            entity.MonthlyCategoryEntries = new List<MonthlyReport.MonthlyCategoryEntry>();
            entity.TotalIn = 0;
            entity.TotalOut = 0;

            foreach (KeyValuePair<string, decimal> keyVal in aggregatedCategoryAmountDict)
            {
                string categoryId = keyVal.Key;
                decimal entryAmount = keyVal.Value;

                if (!categoryDict.TryGetValue(categoryId, out Category? category))
                    continue;

                switch (category.Action)
                {
                    case CategoryActionEnum.Credit:
                        entity.TotalOut += entryAmount;
                        break;
                    case CategoryActionEnum.Debit:
                        entity.TotalIn += entryAmount;
                        break;
                }

                entity.MonthlyCategoryEntries.Add(
                    new MonthlyReport.MonthlyCategoryEntry(categoryId, entryAmount));
            }

            entity.SavedAmount = entity.TotalIn - entity.TotalOut;

            if (entity.SavedAmount > 0)
                entity.SavedPercent = (entity.SavedAmount / userProfile.CurrentNetSalary * 100) * 100;

            decimal savingsLimitAmount = userProfile.CurrentNetSalary * (userProfile.TargetSavingsPercent / 100);
            entity.RemainingAmount = userProfile.CurrentNetSalary - (savingsLimitAmount + entity.TotalOut);
           
            if(entity.RemainingAmount < 0)
                entity.RemainingAmount = 0;

            entity.SavedFromSalary = userProfile.CurrentNetSalary - entity.TotalOut;

            return entity;
        }

        public async Task<bool> UpdateBasicDetails(UpdateMonthlyReportBasicDetailsRequestVM request)
        {
            MonthlyReport entity = await _monthlyReportRepo.GetById(request.Id);

            if (entity == null)
            {
                throw new ApplicationException("Monthly report not found.");
            }

            return await _monthlyReportRepo.UpdateBasicDetails(request.Id, request.TotalFunds, request.Remarks);
        }


    }
}
