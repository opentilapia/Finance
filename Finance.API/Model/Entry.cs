using Finance.API.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Model
{
    public class Entry
    {
        public Entry(UpsertEntryRequestVM request)
        {
            EntryDate = request.EntryDate;
            Amount = request.Amount;
            Description = request.Description;
            Remarks = request.Remarks;
            
            ObjectId.TryParse(request.CategoryId, out ObjectId categoryId);
            CategoryId = categoryId;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Amount { get; set; }
        public string Description {get;set;}
        public string Remarks {get;set;}
        public ObjectId CategoryId {get;set;}
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
