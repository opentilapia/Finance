using Finance.API.Common;
using Finance.API.DataService.Interface;
using MongoDB.Driver;
using Entity = Finance.API.Domain.Class.Entry;

namespace Finance.API.DataService
{
    public class EntryRepository : BaseMongoDbRepository<Entity>, IEntryRepository
    {
        private const string COLLECTION_NAME = "entries";

        public EntryRepository(IMongoDatabase db) 
            :base(db, COLLECTION_NAME)
        {
        }

        public async Task<Entity> GetById(string id)
        {
            var filter = Builders<Entity>.Filter.Eq(s => s.Id, id);

            return await FindOne(filter);
        }
        public async Task<List<Entity>> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var filter = Builders<Entity>.Filter.Gte(s => s.EntryDate, startDate) &
                Builders<Entity>.Filter.Lt(s => s.EntryDate, endDate);

            return await FindMany(filter);
        }

        public async Task<List<Entity>> GetPaginated(string categoryId, int pageSize, DateTime lastEntryDate)
        {
            var filter = Builders<Entity>.Filter.Lt(s => s.EntryDate, lastEntryDate) &
                Builders<Entity>.Filter.Eq(s => s.CategoryId, categoryId);

            var sort = Builders<Entity>.Sort.Descending(s => s.EntryDate);

            return await FindMany(filter, sort: sort, limit: pageSize);
        }

        public async Task<bool> Upsert(Entity entity)
        {
            if (entity.Id == null)
                entity.Id = GetPKId();

            var filter = Builders<Entity>.Filter.Eq(s => s.Id, entity.Id);

            DateTime currDate = DateHelper.GetDateTimePH();

            var update = Builders<Entity>.Update
                .Set(s => s.EntryDate, entity.EntryDate)
                .Set(s => s.Amount, entity.Amount)
                .Set(s => s.Remarks, entity.Remarks)
                .Set(s => s.CategoryId, entity.CategoryId)
                .Set(s => s.LastUpdatedDate, currDate)
                .SetOnInsert(s => s.CreatedDate, currDate);

            var updateOptions  = new UpdateOptions()
            {
                IsUpsert = true,
            };

            return await Update(filter, update, updateOptions); 
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<Entity>.Filter.Eq(s => s.Id, id);

            return await DeleteOne(filter);
        }

        public async Task<bool> BatchInsert(List<Entity> entities)
        {
            entities.ForEach(s => s.Id = GetPKId());

            return await InsertMany(entities);
        }
    }
}
