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

        /// <summary>
        /// SavedAmount / CurrentNetSalary
        /// </summary>
        public decimal SavedPercent { get; set; }

        /// <summary>
        /// Total In - Total Out
        /// </summary>
        public decimal SavedAmount { get; set; }

        /// <summary>
        /// CurrentNetSalary - (Limit + Total Out)
        /// </summary>
        public decimal RemainingAmount { get; set; }

        /// <summary>
        /// CurrentNetSalary - Total Out
        /// </summary>
        public decimal SavedFromSalary { get; set; }
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
