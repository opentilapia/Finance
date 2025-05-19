using Finance.API.Domain.Enum;
using Finance.API.Domain.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Domain.Class
{
    public class Category : BaseEntity
    {
        public Category()
        {

        }

        public Category(UpsertCategoryRequestVM request)
        {
            CategoryName = request.CategoryName;
            ColorCoding = request.ColorCoding;
            Action = request.Action;
        }

        public string CategoryName { get; set; }

        public string ColorCoding { get; set; }
        
        public CategoryActionEnum Action { get; set; }

    }
}
