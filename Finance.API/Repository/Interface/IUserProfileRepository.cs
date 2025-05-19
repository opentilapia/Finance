using Finance.API.Domain.Class;

namespace Finance.API.DataService.Interface
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> GetById(string id);
        Task<bool> Upsert(UserProfile entity);
    }
}
