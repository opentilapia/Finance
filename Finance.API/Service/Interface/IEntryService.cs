using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;


namespace Finance.API.Service.Interface
{
    public interface IEntryService
    {
        Task<bool> Upsert(UpsertEntryRequestVM entity);
        Task<EntryVM> GetById(string id);
        Task<List<EntryVM>> GetPaginated(string categoryId, DateTime? lastEntryDate);
        Task<bool> Delete(string id);
        Task<bool> Import(string categoryId, IFormFile file);
    }
}
