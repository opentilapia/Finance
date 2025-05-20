using Finance.API.Domain.Class;
using Finance.API.Domain.Enum;

namespace Finance.API.Domain.ViewModel
{
    public class AccountOverviewVM
    {
        public AccountOverviewVM()
        {

        }

        public List<AccountVM> Accounts { get; set; }

        public decimal TotalFlexibleMoney { get; set; }

        public decimal TotalLongTermMoney { get; set; }

        public decimal TotalHighInterestMoney { get; set; }

        public decimal SpendableCreditCardAmount { get; set; }

        public decimal TotalPayables { get; set; }

        public decimal TotalReceivables { get; set; }

        // Asset classes
        public decimal TotalCash { get; set; }

        public decimal TotalEquities { get; set; }

        public decimal TotalFixedIncome { get; set; }
    }
}
