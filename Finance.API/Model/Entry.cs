using Finance.API.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Model
{
    public class Entry
    {
        public Entry(UpsertEntryRequestVM request) 
        { 
            Description = request.Description;
            EntryDate = request.EntryDate;
            Remarks = request.Remarks;
            
            ObjectId.TryParse(request.CategoryId, out ObjectId categoryId);
            CategoryId = categoryId;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        public string Description {get;set;}

        public DateTime EntryDate { get; set; }

        public string Remarks {get;set;}

        public ObjectId CategoryId {get;set;}

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
