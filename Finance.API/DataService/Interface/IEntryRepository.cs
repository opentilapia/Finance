using Finance.API.Model;
using Finance.API.ViewModel;

namespace Finance.API.DataService.Interface
{
    public interface IEntryRepository: IRepository<Category>
    {

        Task<List<Entry>> GetPaginated(int pageSize, DateTime lastCreatedDate);

        Task<Entry> GetById(string id);

        Task<bool> Upsert(Entry entity);

        Task<bool> Delete(string id);
    }
}
