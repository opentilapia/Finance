using Finance.API.Domain.Enum;
using Finance.API.Domain.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Domain.Class
{
    public class Category
    {
        public Category()
        {

        }

        public Category(UpsertCategoryRequestVM request)
        {
            CategoryName = request.CategoryName;
            ColorCoding = request.ColorCoding;
            CreatedDate = DateTime.Now;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string CategoryName { get; set; }

        public string ColorCoding { get; set; }
        
        public CategoryActionEnum Action { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
