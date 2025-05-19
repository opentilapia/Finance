using Finance.API.Common;
using Finance.API.DataService.Interface;
using MongoDB.Driver;
using Entity = Finance.API.Domain.Class.Category;

namespace Finance.API.DataService
{
    public class CategoryRepository : BaseMongoDbRepository<Entity>, ICategoryRepository
    {
        private const string COLLECTION_NAME = "categories";

        public CategoryRepository(IMongoDatabase db)
            : base(db, COLLECTION_NAME)
        {
        }

        public async Task<Entity> GetById(string id)
        {
            var filter = Builders<Entity>.Filter.Eq(s => s.Id, id);

            return await FindOne(filter);
        }
        public async Task<List<Entity>> GetAll()
        {
            var filter = Builders<Entity>.Filter.Empty;

            return await FindMany(filter);
        }

        public async Task<bool> IsExistById(string id)
        {
            var filter = Builders<Entity>.Filter.Eq(s => s.Id, id);

            return await IsExist(filter);
        }

        public async Task<bool> Upsert(Entity entity)
        {
            if (entity.Id == null)
                entity.Id = GetPKId();

            var filter = Builders<Entity>.Filter.Eq(s => s.Id, entity.Id);

            DateTime currDate = DateHelper.GetDateTimePH();

            var update = Builders<Entity>.Update
                .Set(s => s.CategoryName, entity.CategoryName)
                .Set(s => s.Action, entity.Action)
                .Set(s => s.ColorCoding, entity.ColorCoding)
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
    }
}
