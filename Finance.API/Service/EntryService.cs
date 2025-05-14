using Finance.API.DataService.Interface;
using Finance.API.Model;
using Finance.API.Service.Interface;
using Finance.API.ViewModel;
using Finance.API.ViewModel.Request;

namespace Finance.API.Service
{
    public class EntrySevice : IEntryService
    {
        readonly IEntryRepository _repo;
        readonly ICategoryRepository _categoryRepo;

        const int PAGE_SIZE = 10;

        public EntrySevice(IEntryRepository repo, ICategoryRepository categoryRepo)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
        }

        public async Task<bool> Upsert(UpsertEntryRequestVM request)
        {
            if (!request.Id.IsNullOrEmpty() && 
                !request.CategoryId.IsNullOrEmpty())
            {
                throw new ApplicationException("Category ID cannot be empty when creating entry.");
            }

            if (!request.CategoryId.IsNullOrEmpty() && 
                !await _categoryRepo.IsExistById(request.CategoryId))
            {
                throw new ApplicationException("Category ID is required.");
            }

            Entry entity = new Entry(request);
            entity.CreatedDate = DateTime.Now;

            return await _repo.Upsert(entity);
        }

        public async Task<EntryVM> GetById(string id)
        {
            Entry entity = await _repo.GetById(id);

            if (entity == null)
            {
                throw new ApplicationException("Not found");
            }

            return new EntryVM(entity);
        }

        public async Task<List<EntryVM>> GetPaginated(DateTime? lastEntryDate)
        {
            if (!lastEntryDate.HasValue)
            {
                lastEntryDate = DateTime.MinValue;
            }

            List<Entry> sources = await _repo.GetPaginated(PAGE_SIZE, lastEntryDate.Value);
            List<EntryVM> result = new List<EntryVM>();
            
            foreach (Entry entity in sources)
            {
                result.Add(new EntryVM(entity));
            }

            return result;
        }

        public async Task<bool> Delete(string id)
        {
            return await _repo.Delete(id);
        }
    }
}
