using Finance.API.Common;
using Finance.API.DataService.Interface;
using Finance.API.Domain.Class;
using Finance.API.Domain.Enum;
using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;
using Finance.API.Repository.Interface;
using Finance.API.Service.Interface;

namespace Finance.API.Service
{
    public class MonthlyReportService : IMonthlyReportService
    {
        private readonly IMonthlyReportRepository _monthlyReportRepo;
        private readonly IEntryRepository _entryRepo;
        private readonly ICategoryRepository _categoryRepo;

        const int PAGE_SIZE = 10;

        public MonthlyReportService(IMonthlyReportRepository repo, IEntryRepository entryRepo, ICategoryRepository categoryRepo)
        {
            _monthlyReportRepo = repo;
            _entryRepo = entryRepo;
            _categoryRepo = categoryRepo;
        }

        public async Task<List<MonthlyReportVM>> GetPaginated(DateTime? lastMonthEntry)
        {
            if (!lastMonthEntry.HasValue)
            {
                lastMonthEntry = DateTime.MinValue;
            }

            List<MonthlyReport> sources = await _monthlyReportRepo.GetPaginated(PAGE_SIZE, lastMonthEntry.Value);
            List<MonthlyReportVM> result = new List<MonthlyReportVM>();

            foreach (MonthlyReport entity in sources)
            {
                result.Add(new MonthlyReportVM(entity));
            }

            return result;
        }

        public async Task<bool> Compute()
        {
            DateTime currDate = DateHelper.GetDateTimePH();
            DateTime startDate = currDate.GetStartOfMonth();
            DateTime endDate = currDate.GetEndOfMonth(); 

            MonthlyReport entity = await _monthlyReportRepo.GetByMonth(startDate);

            if (entity == null)
            {
                entity = new MonthlyReport();
                entity.MonthlyCategoryEntries = new List<MonthlyReport.MonthlyCategoryEntry>();
                entity.Month = startDate;
            }
            
            entity = await compute(entity, startDate, endDate);
            Console.WriteLine(entity.Month);
            return await _monthlyReportRepo.Insert(entity);
        }

        public async Task<bool> Recompute(string id)
        {
            MonthlyReport entity = await _monthlyReportRepo.GetById(id);

            if (entity == null)
            {
                throw new ApplicationException("Monthly report not found.");
            }

            entity = await compute(entity,
                entity.Month.GetStartOfMonth(),
                entity.Month.GetEndOfMonth());

            return await _monthlyReportRepo.UpdateByComputation(entity);
        }

        private async Task<MonthlyReport> compute(MonthlyReport entity, DateTime startDate, DateTime endDate)
        {
            List<Entry> entryList = await _entryRepo.GetByDateRange(startDate.GetStartOfMonth(), endDate.GetEndOfMonth());
            Dictionary<string, decimal> categoryIdAmountDict = new Dictionary<string, decimal>();

            foreach (Entry entry in entryList)
            {
                if (categoryIdAmountDict.TryGetValue(entry.CategoryId, out decimal amount))
                    categoryIdAmountDict[entry.CategoryId] = amount + entry.Amount;
                else
                    categoryIdAmountDict.Add(entry.CategoryId, entry.Amount);
            }

            List<Category> categoryList = await _categoryRepo.GetAll();
            Dictionary<string, Category> categoryDict = categoryList.ToDictionary(s => s.Id, s => s);

            foreach (KeyValuePair<string, decimal> keyVal in categoryIdAmountDict)
            {
                string categoryId = keyVal.Key;
                decimal entryAmount = keyVal.Value;

                if (!categoryDict.TryGetValue(categoryId, out Category category))
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

            return entity;
        }

        public async Task<bool> UpdateBasicDetails(UpdateMonthlyReportBasicDetailsRequestVM request)
        {
            throw new NotImplementedException();
        }


    }
}
