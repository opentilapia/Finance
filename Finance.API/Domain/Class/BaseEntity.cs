using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Finance.API.Domain.Class
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
