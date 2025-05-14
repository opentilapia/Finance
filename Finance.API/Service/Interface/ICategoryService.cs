using Finance.API.Model;

namespace Finance.API.Service.Interface
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(Category entity);

        Task<Category> GetCategoryById(string categoryId);

        Task<List<Category>> GetAllCategories();
    }
}
