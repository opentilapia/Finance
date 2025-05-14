using Finance.API.Domain.Class;

namespace Finance.API.DataService.Interface
{
    public interface IEntryRepository
    {
        Task<List<Entry>> GetPaginated(int pageSize, DateTime lastCreatedDate);
        Task<Entry> GetById(string id);
        Task<bool> Upsert(Entry entity);
        Task<bool> Delete(string id);
    }
}
