using Finance.API.Common;
using Finance.API.Common.Interface;
using Finance.API.DataService.Interface;
using Finance.API.Domain.Class;
using Finance.API.Domain.ViewModel;
using Finance.API.Domain.ViewModel.Request;
using Finance.API.Service.Interface;

namespace Finance.API.Service
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _repo;
        private const string USER_PROFILE_PERSISTENT_ID = "682b514b0622febbbfa4bb9e";

        public UserProfileService(IUserProfileRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Upsert(UpsertUserProfileRequestVM request)
        {
            UserProfile entity = new UserProfile(request);
            entity.Id = USER_PROFILE_PERSISTENT_ID;
            entity.CreatedDate = DateHelper.GetDateTimePH();

            return await _repo.Upsert(entity);
        }

        public async Task<UserProfileVM> GetById(string id)
        {
            UserProfile entity = await _repo.GetById(id);

            if (entity == null)
            {
                throw new ApplicationException("User not found");
            }

            return new UserProfileVM(entity);
        }
    }
}
