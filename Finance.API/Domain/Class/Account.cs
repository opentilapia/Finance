using Finance.API.Domain.Enum;
using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Domain.Class
{
    public class Account : BaseEntity
    {
        public Account(UpsertAccountRequestVM entity)
        {
            Name = entity.Name;
            Amount = entity.CurrentFunds.Value;
            GrossInterestRate = entity.GrossInterestRate;
            Type = entity.Type.Value;
        }

        public string Name { get;set; }
        public decimal Amount { get;set; }

        /// <summary>
        /// Only applicable if Time deposit or HYSA
        /// </summary>
        public decimal GrossInterestRate { get; set; }
        public AccountTypeEnum Type { get; set; }
    }
}
