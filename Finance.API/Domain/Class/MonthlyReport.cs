using Finance.API.Domain.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Domain.Class
{
    public class MonthlyReport
    {
        //public MonthlyReport(UpsertEntryRequestVM request)
        //{
        //    EntryDate = request.EntryDate;
        //    Amount = request.Amount;
        //    Description = request.Description;
        //    Remarks = request.Remarks;

        //    ObjectId.TryParse(request.CategoryId, out ObjectId categoryId);
        //    CategoryId = categoryId;
        //}

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Month { get; set; }
        public decimal TotalFunds { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }


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
