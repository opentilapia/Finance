using Finance.API.Domain.Enum;
using Finance.API.Domain.ViewModel.Request;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Finance.API.Domain.Class
{
    public class UserProfile : BaseEntity
    {
        public UserProfile()
        {

        }

        public UserProfile(UpsertUserProfileRequestVM request)
        {
            Nickname = request.Nickname;
            CurrentGrossSalary = request.CurrentGrossSalary;
            CurrentNetSalary = request.CurrentNetSalary;
            TargetSavingsPercent = request.TargetSavingsPercent;
            CreditLimit = request.CreditLimit;
        }

        public string Nickname { get; set; }
        public decimal CurrentGrossSalary { get; set; }
        public decimal CurrentNetSalary { get; set; }
        public decimal TargetSavingsPercent { get; set; }
        public decimal CreditLimit { get; set; }

    }
}
