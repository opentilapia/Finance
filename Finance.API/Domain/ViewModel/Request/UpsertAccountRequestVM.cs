using Finance.API.Domain.Enum;

namespace Finance.API.Domain.ViewModel.Request
{
    public class UpsertAccountRequestVM
    {

        public string? Id { get; set; }
        public string? Name { get; set; }
        public decimal? CurrentFunds { get; set; }
        public decimal GrossInterestRate { get; set; }
        public AccountTypeEnum? Type { get; set; }
    }
}
