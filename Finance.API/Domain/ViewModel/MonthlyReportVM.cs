using Finance.API.Domain.Class;
using Finance.API.Domain.Enum;

namespace Finance.API.Domain.ViewModel
{
    public class MonthlyReportVM
    {
        public MonthlyReportVM(MonthlyReport entity) 
        { 
            Id = entity.Id;
            Month = entity.Month;
            TotalFunds = entity.TotalFunds;
            Remarks = entity.Remarks;
            SavingsPercent = entity.SavingsPercent;
            Remaining = entity.Remaining;
            TotalIn = entity.TotalIn;
            TotalOut = entity.TotalOut;
        }

        public string Id { get; set; }
        public DateTime Month { get; set; }
        public string Remarks { get; set; }

        // Computed server side
        public decimal TotalFunds { get; set; }
        public decimal SavingsPercent { get; set; }
        public decimal Remaining { get; set; }
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
        public List<MonthlyCategoryEntryVM> MonthlyCategoryEntries { get; set; }


        public class MonthlyCategoryEntryVM
        {
            public string CategoryId { get; set; }

            public decimal Total { get; set; }

        }
    }
}
