using MongoDB.Driver;
using Finance.API.Model;
using Finance.API.DataService.Interface;
using MongoDB.Bson;

namespace Finance.API.DataService
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(IMongoDatabase db)
        {
            _categories = db.GetCollection<Category>("category");
        }
        public async Task<Category> GetById(string id)
        {
            ObjectId.TryParse(id, out ObjectId mongoDbId);
            var filter = Builders<Category>.Filter.Eq(s => s.Id, mongoDbId);

            return await _categories.Find(filter)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Category>> GetAll()
        {
            var filter = Builders<Category>.Filter.Empty;

            return await _categories
                .Find(filter)
                .ToListAsync();
        }

        public async Task<bool> Insert(Category entity)
        {
            await _categories.InsertOneAsync(entity);

            return true;
        }

        public async Task<bool> Update(Category entity)
        {
            var filter = Builders<Category>.Filter.Eq(s => s.Id, entity.Id);
            var update = Builders<Category>.Update
                .Set(s => s.CategoryName, entity.CategoryName)
                .Set(s => s.LastUpdatedDate, DateTime.Now);

            var result = await _categories.UpdateOneAsync(filter, update);

            return true;
        }

        
    }
}
