using Finance.API.Domain.Class;
using Finance.API.Domain.Enum;

namespace Finance.API.Domain.ViewModel
{
    public class AccountVM
    {
        public AccountVM(Account entity) 
        { 
            Id = entity.Id;
            Name = entity.Name;
            CurrentFunds = entity.CurrentFunds;
            GrossInterestRate = entity.GrossInterestRate;
            Type = entity.Type;
            CreatedDate = entity.CreatedDate;
            LastUpdatedDate = entity.LastUpdatedDate;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public decimal CurrentFunds { get; set; }
        public decimal GrossInterestRate { get; set; }
        public AccountTypeEnum Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
