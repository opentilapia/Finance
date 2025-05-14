using Finance.API.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Model
{
    public class Category
    {
        public Category() 
        { 

        }

        public Category(CreateCategoryRequestVM request) 
        { 
            CategoryName = request.CategoryName;
            ColorCoding = request.ColorCoding;
            CreatedDate = DateTime.Now;
        }

        [BsonId]
        public ObjectId Id { get; set; }

        public string CategoryName { get; set; }

        public string ColorCoding { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
