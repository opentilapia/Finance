using Finance.API.Model;
using Finance.API.ViewModel;
using Finance.API.ViewModel.Request;

namespace Finance.API.Service.Interface
{
    public interface ICategoryService
    {
        Task<bool> Upsert(UpsertCategoryRequestVM entity);

        Task<CategoryVM> GetById(string categoryId);

        Task<List<CategoryVM>> GetAll();

        Task<bool> Delete(string id);
    }
}
