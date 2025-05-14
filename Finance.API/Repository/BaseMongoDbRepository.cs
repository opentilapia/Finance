using Finance.API.DataService.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

public class BaseMongoDbRepository<T>: IBaseRepository where T : class
{
    protected readonly IMongoCollection<T> _collection;

    public BaseMongoDbRepository(IMongoDatabase db, string collectionName)
    {
        _collection = db.GetCollection<T>(collectionName);
    }

    public string GetPKId()
    {
        return ObjectId.GenerateNewId()
            .ToString();
    }

    public async Task<bool> IsExist(FilterDefinition<T> filter)
    {
        return await _collection.CountDocumentsAsync(filter) > 0;
    }
}