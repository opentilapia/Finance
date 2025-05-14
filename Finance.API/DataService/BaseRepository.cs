using MongoDB.Bson;

public class BaseRepository
{
    protected string CollectionName = "";

    protected virtual string ToIdString(ObjectId id)
    {
        return id.ToString();
    }

    protected virtual ObjectId? ToObjectId(string id)
    {
        if(ObjectId.TryParse(id, out ObjectId result))
            return result;

        return null;
    }

    protected virtual string GetPKId()
    {
        return ObjectId.GenerateNewId()
            .ToString();
    }
}