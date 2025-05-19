using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;


namespace Finance.API.Service.Interface
{
    public interface IUserProfileService
    {
        Task<UserProfileVM> GetById(string id);
        Task<bool> Upsert(UpsertUserProfileRequestVM request);
    }
}
