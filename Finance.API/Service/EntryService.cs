using Finance.API.Common;
using Finance.API.Common.Interface;
using Finance.API.DataService.Interface;
using Finance.API.Domain.Class;
using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;
using Finance.API.Service.Interface;

namespace Finance.API.Service
{
    public class EntrySevice : IEntryService
    {
        private readonly IEntryRepository _repo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly IFileParser<Entry> _fileParser;

        const int PAGE_SIZE = 10;

        public EntrySevice(IEntryRepository repo, ICategoryRepository categoryRepo, IFileParser<Entry> fileParser)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
            _fileParser = fileParser;
        }

        public async Task<bool> Upsert(UpsertEntryRequestVM request)
        {
            if (request.Id.IsNullOrEmpty() && 
                request.CategoryId.IsNullOrEmpty())
            {
                throw new ApplicationException("Category ID cannot be empty when creating entry.");
            }

            if (!request.CategoryId.IsNullOrEmpty() && 
                !await _categoryRepo.IsExistById(request.CategoryId))
            {
                throw new ApplicationException("Category ID is required.");
            }

            Entry entity = new Entry(request);
            entity.CreatedDate = DateHelper.GetDateTimePH();

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

        public async Task<List<EntryVM>> GetPaginated(string categoryId, DateTime? lastEntryDate)
        {
            if (!lastEntryDate.HasValue)
                lastEntryDate = DateTime.MaxValue;

            List<Entry> sources = await _repo.GetPaginated(categoryId, PAGE_SIZE, lastEntryDate.Value);
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

        public async Task<bool> Import(string categoryId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ApplicationException("File is required");
            }

            if(!await _categoryRepo.IsExistById(categoryId))
            {
                throw new ApplicationException("Category not found.");
            }

            List<Entry> entities = await _fileParser.Parse(file);

            if (entities.Count == 0)
            {
                throw new ApplicationException("No lines found.");
            }

            entities.ForEach(s => s.CategoryId = categoryId); 

            return await _repo.BatchInsert(entities);
        }
    }
}
