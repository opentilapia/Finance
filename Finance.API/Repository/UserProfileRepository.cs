using Finance.API.Common;
using Finance.API.DataService.Interface;
using Finance.API.Domain.Class;
using MongoDB.Driver;
using Entity = Finance.API.Domain.Class.UserProfile;

namespace Finance.API.DataService
{
    public class UserProfileRepository : BaseMongoDbRepository<Entity>, IUserProfileRepository
    {
        private const string COLLECTION_NAME = "user_profiles";

        public UserProfileRepository(IMongoDatabase db) 
            :base(db, COLLECTION_NAME)
        {
        }

        public async Task<Entity> GetById(string id)
        {
            var filter = Builders<Entity>.Filter.Eq(s => s.Id, id);

            return await FindOne(filter);
        }

        public async Task<bool> Upsert(Entity entity)
        {
            if (entity.Id == null)
                entity.Id = GetPKId();

            var filter = Builders<Entity>.Filter.Eq(s => s.Id, entity.Id);

            DateTime currDate = DateHelper.GetDateTimePH();

            var update = Builders<Entity>.Update
                .Set(s => s.Nickname, entity.Nickname)
                .Set(s => s.CurrentGrossSalary, entity.CurrentGrossSalary)
                .Set(s => s.CurrentNetSalary, entity.CurrentNetSalary)
                .Set(s => s.TargetSavingsPercent, entity.TargetSavingsPercent)
                .Set(s => s.CreditLimit, entity.CreditLimit)
                .Set(s => s.LastUpdatedDate, currDate)
                .SetOnInsert(s => s.CreatedDate, currDate);

            var updateOptions  = new UpdateOptions()
            {
                IsUpsert = true,
            };

            return await Update(filter, update, updateOptions); 
        }
    }
}
