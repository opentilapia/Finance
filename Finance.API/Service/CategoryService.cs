using Finance.API.Common;
using Finance.API.DataService.Interface;
using Finance.API.Domain.Class;
using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;
using Finance.API.Service.Interface;

namespace Finance.API.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task<bool> Upsert(UpsertCategoryRequestVM request)
        {
            Category entity = new Category(request);
            entity.CreatedDate = DateHelper.GetDateTimePH();

            return await _categoryRepo.Upsert(entity);
        }

        public async Task<CategoryVM> GetById(string id)
        {
            Category entity = await _categoryRepo.GetById(id);

            if (entity == null)
            {
                throw new ApplicationException("Not found");
            }

            return new CategoryVM(entity);
        }

        public async Task<List<CategoryVM>> GetAll()
        {
            List<Category> sources = await _categoryRepo.GetAll();
            List<CategoryVM> result = new List<CategoryVM>();

            foreach (Category category in sources)
            {
                result.Add(new CategoryVM(category));
            }

            return result;
        }

        public async Task<bool> Delete(string id)
        {
            return await _categoryRepo.Delete(id);
        }
    }
}
