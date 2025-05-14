using Finance.API.DataService.Interface;
using MongoDB.Driver;
using CurrentClass = Finance.API.Model.Entry;

namespace Finance.API.DataService
{
    public class EntryRepository : BaseRepository, IEntryRepository
    {
        const string COLLECTION_NAME = "entry";
        readonly IMongoCollection<CurrentClass> _collection;

        public EntryRepository(IMongoDatabase db)
        {
            _collection = db.GetCollection<CurrentClass>(COLLECTION_NAME);
        }

        public async Task<CurrentClass> GetById(string id)
        {
            var filter = Builders<CurrentClass>.Filter.Eq(s => s.Id, id);

            return await _collection
                .Find(filter)
                .FirstOrDefaultAsync();
        }
        public async Task<List<CurrentClass>> GetPaginated(int pageSize, DateTime lastEntryDate)
        {
            var filter = Builders<CurrentClass>.Filter.Gte(s => s.EntryDate, lastEntryDate);
            var sort = Builders<CurrentClass>.Sort.Descending(s => s.EntryDate);

            return await _collection
                .Find(filter)
                .Sort(sort)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<bool> Upsert(CurrentClass entity)
        {
            if (entity.Id == null)
            {
                entity.Id = GetPKId();
            }

            var filter = Builders<CurrentClass>.Filter.Eq(s => s.Id, entity.Id);

            DateTime currDate = DateTime.Now;

            var update = Builders<CurrentClass>.Update
                .Set(s => s.EntryDate, entity.EntryDate)
                .Set(s => s.Amount, entity.Amount)
                .Set(s => s.Description, entity.Description)
                .Set(s => s.Remarks, entity.Remarks)
                .Set(s => s.CategoryId, entity.CategoryId)
                .Set(s => s.LastUpdatedDate, currDate)
                .SetOnInsert(s => s.CreatedDate, currDate);

            var updateOptions  = new UpdateOptions()
            {
                IsUpsert = true,
            };

            await _collection.UpdateOneAsync(filter, update, updateOptions);
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<CurrentClass>.Filter.Eq(s => s.Id, id);
            await _collection.DeleteOneAsync(filter);

            return true;
        }
    }
}
