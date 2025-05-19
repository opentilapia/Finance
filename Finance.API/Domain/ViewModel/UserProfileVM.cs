using Finance.API.Domain.Class;

namespace Finance.API.Domain.ViewModel
{
    public class UserProfileVM
    {
        public UserProfileVM(UserProfile entity) 
        { 
            Id = entity.Id;
            Nickname = entity.Nickname;
            CurrentGrossSalary = entity.CurrentGrossSalary;
            CurrentNetSalary = entity.CurrentNetSalary;
            TargetSavingsPercent = entity.TargetSavingsPercent;
            CreditLimit = entity.CreditLimit;
        }

        public string Id { get; set; }
        public string Nickname { get; set; }
        public decimal CurrentGrossSalary { get; set; }
        public decimal CurrentNetSalary { get; set; }
        public decimal TargetSavingsPercent { get; set; }
        public decimal CreditLimit { get; set; }
    }
}
