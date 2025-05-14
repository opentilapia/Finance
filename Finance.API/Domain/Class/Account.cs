using Finance.API.Domain.Enum;
using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Domain.Class
{
    public class Account
    {
        public Account(UpsertAccountRequestVM entity)
        {
            Name = entity.Name;
            CurrentFunds = entity.CurrentFunds.Value;
            GrossInterestRate = entity.GrossInterestRate;
            Type = entity.Type.Value;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get;set; }
        public decimal CurrentFunds { get;set; }

        /// <summary>
        /// Only applicable if Time deposit or HYSA
        /// </summary>
        public decimal GrossInterestRate { get; set; }
        public AccountTypeEnum Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
