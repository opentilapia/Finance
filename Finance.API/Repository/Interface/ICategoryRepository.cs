using Finance.API.Domain.Class;

namespace Finance.API.DataService.Interface
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<Category> GetById(string id);
        Task<bool> Upsert(Category entity);
        Task<bool> Delete(string id);
        Task<bool> IsExistById(string id);
    }
}
