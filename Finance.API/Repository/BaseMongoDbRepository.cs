using Finance.API.DataService.Interface;
using MongoDB.Bson;
using MongoDB.Driver;

public class BaseMongoDbRepository<T> : IBaseRepository where T : class
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

    public async Task<bool> Update(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions? updateOptions = null)
    {
        var result = await _collection.UpdateManyAsync(filter, update, updateOptions);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> InsertOne(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return true;
    }

    public async Task<bool> DeleteOne(FilterDefinition<T> filter)
    {
        var result = await _collection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }

    public async Task<bool> DeleteMany(FilterDefinition<T> filter)
    {
        var result = await _collection.DeleteManyAsync(filter);

        return result.DeletedCount > 0;
    }

    public async Task<List<T>> FindMany(FilterDefinition<T> filter, ProjectionDefinition<T>? projection = null, SortDefinition<T>? sort = null, int limit = 0)
    {
        var result = baseFind(filter, projection, sort, limit);

        if (result == null)
            return new List<T>();

        return await result.ToListAsync();
    }

    public async Task<T> FindOne(FilterDefinition<T> filter, ProjectionDefinition<T>? projection = null, SortDefinition<T>? sort = null, int limit = 0)
    {
        var result = baseFind(filter, projection, sort, limit);

        if (result == null)
            return null;

        return await result.FirstOrDefaultAsync();
    }

    private FindFluentBase<T, T> baseFind(FilterDefinition<T> filter, ProjectionDefinition<T>? projection = null, SortDefinition<T>? sort = null, int limit = 0)
    {
        var result = _collection.Find(filter);

        if (projection != null)
            result = result.Project<T>(projection);

        if (sort != null)
            result = result.Sort(sort);

        if (limit > 0)
            result = result.Limit(limit);

        return (FindFluentBase<T, T>)result;
    }
}