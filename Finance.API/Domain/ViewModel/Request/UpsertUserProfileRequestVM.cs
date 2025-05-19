namespace Finance.API.Domain.ViewModel.Request
{
    public class UpsertUserProfileRequestVM
    {
        public required string Nickname { get; set; }
        public required decimal CurrentGrossSalary { get; set; }
        public required decimal CurrentNetSalary { get; set; }
        public required decimal TargetSavingsPercent { get; set; }
        public decimal CreditLimit { get; set; }
    }
}
