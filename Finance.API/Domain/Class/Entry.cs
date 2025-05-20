using Finance.API.Domain.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Domain.Class
{
    public class Entry : BaseEntity
    {
        public Entry() { }

        public Entry(UpsertEntryRequestVM request)
        {
            EntryDate = request.EntryDate;
            Amount = request.Amount;
            Remarks = request.Remarks;
            CategoryId = request.CategoryId;
        }

        public DateTime EntryDate { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
    }
}
