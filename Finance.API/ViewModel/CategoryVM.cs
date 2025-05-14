using Finance.API.Model;

namespace Finance.API.ViewModel
{
    public class CategoryVM
    {
        public CategoryVM(Category entity) 
        { 
            Id = entity.Id.ToString();
            CategoryName = entity.CategoryName;
            ColorCoding = entity.ColorCoding;
            CreatedDate = entity.CreatedDate;
            LastUpdatedDate = entity.LastUpdatedDate;
        }

        public string Id { get; set; }
        public string CategoryName { get; set; }
        public string ColorCoding { get; set; }
        public DateTime CreatedDate { get; set; }   
        public DateTime LastUpdatedDate { get; set; }
    }
}
