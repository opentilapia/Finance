using Finance.API.Model;
using Finance.API.ViewModel;

namespace Finance.API.DataService.Interface
{
    public interface ICategoryRepository: IRepository<Category>
    {

        Task<List<Category>> GetAll();

        Task<Category> GetById(string id);

        Task<bool> Upsert(Category entity);

        Task<bool> Delete(string id);

        Task<bool> IsExistById(string id);
    }
}
