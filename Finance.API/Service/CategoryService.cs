using Finance.API.DataService.Interface;
using Finance.API.Model;
using Finance.API.Service.Interface;
using Finance.API.ViewModel;

namespace Finance.API.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<bool> CreateCategory(Category entity)
        {
            entity.CreatedDate = DateTime.Now;

            return await _categoryRepo.Insert(entity);
        }

        public async Task<Category> GetCategoryById(string id)
        {
            return await _categoryRepo.GetById(id);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _categoryRepo.GetAll();
        }
    }
}
