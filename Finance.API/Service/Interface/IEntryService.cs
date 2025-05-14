using Finance.API.Model;
using Finance.API.ViewModel;
using Finance.API.ViewModel.Request;


namespace Finance.API.Service.Interface
{
    public interface IEntryService
    {
        Task<bool> Upsert(UpsertEntryRequestVM entity);

        Task<EntryVM> GetById(string id);

        Task<List<EntryVM>> GetPaginated(DateTime? lastEntryDate);

        Task<bool> Delete(string id);
    }
}
