using Finance.API.Domain.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Domain.Class
{
    public class MonthlyReport : BaseEntity
    {
        public MonthlyReport() 
        {

        }
        
        public MonthlyReport(UpdateMonthlyReportBasicDetailsRequestVM request)
        {
           TotalFunds = request.TotalFunds;
           Remarks = request.Remarks;
        }

        public DateTime Month { get; set; }
        public decimal TotalFunds { get; set; }
        public string Remarks { get; set; }

        // Computed server side
        public decimal SavingsPercent { get; set; }
        public decimal Remaining { get; set; }
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
        public List<MonthlyCategoryEntry> MonthlyCategoryEntries { get; set; }

        public class MonthlyCategoryEntry
        {
            public MonthlyCategoryEntry(string categoryId, decimal total)
            {
                CategoryId = categoryId;
                Total = total;
            }

            public string CategoryId { get; set; }

            public decimal Total { get; set; }

        }
    }
}
