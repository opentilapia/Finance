using Finance.API.ViewModel;

namespace Finance.API.DataService.Interface
{
    public interface IUserRepository
    {
        Task<bool> Login(string username, string password);
    }
}
