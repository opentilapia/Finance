using Finance.API.DataService.Interface;
using MongoDB.Driver;
using CurrentClass = Finance.API.Domain.Class.Category;

namespace Finance.API.DataService
{
    public class CategoryRepository : BaseMongoDbRepository<CurrentClass>, ICategoryRepository
    {
        private const string COLLECTION_NAME = "category";

        public CategoryRepository(IMongoDatabase db)
            : base(db, COLLECTION_NAME)
        {
        }

        public async Task<CurrentClass> GetById(string id)
        {
            var filter = Builders<CurrentClass>.Filter.Eq(s => s.Id, id);

            return await _collection
                .Find(filter)
                .FirstOrDefaultAsync();
        }
        public async Task<List<CurrentClass>> GetAll()
        {
            var filter = Builders<CurrentClass>.Filter.Empty;

            return await _collection
                .Find(filter)
                .ToListAsync();
        }

        public async Task<bool> IsExistById(string id)
        {
            var filter = Builders<CurrentClass>.Filter.Eq(s => s.Id, id);
            return await IsExist(filter);
        }

        public async Task<bool> Upsert(CurrentClass entity)
        {
            if (entity.Id == null)
                entity.Id = GetPKId();

            var filter = Builders<CurrentClass>.Filter.Eq(s => s.Id, entity.Id);

            DateTime currDate = DateTime.Now;

            var update = Builders<CurrentClass>.Update
                .Set(s => s.CategoryName, entity.CategoryName)
                .Set(s => s.Action, entity.Action)
                .Set(s => s.ColorCoding, entity.ColorCoding)
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
