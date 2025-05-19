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

    protected async Task<bool> IsExist(FilterDefinition<T> filter)
    {
        return await _collection.CountDocumentsAsync(filter) > 0;
    }

    protected async Task<bool> Update(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions? updateOptions = null)
    {
        var result = await _collection.UpdateManyAsync(filter, update, updateOptions);

        if (result.IsAcknowledged)
        {
            if (result.UpsertedId != null)
            {
                return true;
            }

            return result.ModifiedCount > 0;
        }

        return false;
    }

    protected async Task<bool> InsertOne(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return true;
    }

    protected async Task<bool> InsertMany(List<T> entities)
    {
        await _collection.InsertManyAsync(entities);

        return true;
    }
    protected async Task<bool> DeleteOne(FilterDefinition<T> filter)
    {
        var result = await _collection.DeleteOneAsync(filter);

        if (result.IsAcknowledged)
        {
            return result.DeletedCount > 0;
        }

        return false;
    }

    protected async Task<bool> DeleteMany(FilterDefinition<T> filter)
    {
        var result = await _collection.DeleteManyAsync(filter);

        if (result.IsAcknowledged)
        {
            return result.DeletedCount > 0;
        }

        return false;
    }

    protected async Task<List<T>> FindMany(FilterDefinition<T> filter, ProjectionDefinition<T>? projection = null, SortDefinition<T>? sort = null, int limit = 0)
    {
        var result = baseFind(filter, projection, sort, limit);

        if (result == null)
            return new List<T>();

        var finalResult = await result.ToListAsync();

        foreach (T entity in finalResult)
        {
            ConvertDateTimesToManila(entity);
        }

        return finalResult;
    }

    protected async Task<T> FindOne(FilterDefinition<T> filter, ProjectionDefinition<T>? projection = null, SortDefinition<T>? sort = null, int limit = 0)
    {
        var result = baseFind(filter, projection, sort, limit);

        if (result == null)
            return null;

        var entity = await result.FirstOrDefaultAsync();

        ConvertDateTimesToManila(entity);

        return entity;
    }

    private FindFluentBase<T, T> baseFind(FilterDefinition<T> filter, ProjectionDefinition<T>? projection = null, SortDefinition<T>? sort = null, int limit = 0)
    {
        var query = _collection.Find(filter);

        if (projection != null)
            query = query.Project<T>(projection);

        if (sort != null)
            query = query.Sort(sort);

        if (limit > 0)
            query = query.Limit(limit);

        return (FindFluentBase<T, T>)query;
    }

    // Helper method to convert all DateTime properties from UTC to Manila time
    private void ConvertDateTimesToManila(T entity)
    {
        if (entity == null)
            return;

        var manilaTz = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");

        var dateTimeProperties = typeof(T).GetProperties()
            .Where(p => p.PropertyType == typeof(DateTime) && p.CanRead && p.CanWrite);

        foreach (var prop in dateTimeProperties)
        {
            var utcValue = (DateTime)prop.GetValue(entity);

            if (utcValue == DateTime.MinValue)
                continue;

            DateTime utcDateTime = utcValue.Kind == DateTimeKind.Utc
                ? utcValue
                : DateTime.SpecifyKind(utcValue, DateTimeKind.Utc);

            var manilaDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, manilaTz);

            prop.SetValue(entity, manilaDateTime);
        }
    }
}