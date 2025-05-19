using Finance.API.Common;
using Finance.API.Repository.Interface;
using MongoDB.Driver;
using Entity = Finance.API.Domain.Class.MonthlyReport;

namespace Finance.API.DataService
{
    public class MonthlyReportRepository : BaseMongoDbRepository<Entity>, IMonthlyReportRepository
    {
        private const string COLLECTION_NAME = "monthly_reports";

        public MonthlyReportRepository(IMongoDatabase db) 
            :base(db, COLLECTION_NAME)
        {
        }

        public async Task<List<Entity>> GetPaginated(int pageSize, DateTime lastMontEntry)
        {
            var filter = Builders<Entity>.Filter.Lt(s => s.Month, lastMontEntry);

            var sort = Builders<Entity>.Sort.Descending(s => s.Month);

            return await FindMany(filter, sort: sort, limit: pageSize);
        }
        public async Task<bool> Insert(Entity entity)
        {
            entity.Id = GetPKId();
            entity.CreatedDate = DateHelper.GetDateTimePH();
            
            return await InsertOne(entity);
        }

        public async Task<bool> UpdateByComputation(Entity entity)
        {
            var filter = Builders<Entity>.Filter.Eq(s => s.Id, entity.Id);

            var update = Builders<Entity>.Update
                .Set(s => s.TotalIn, entity.TotalIn)
                .Set(s => s.TotalOut, entity.TotalOut)
                .Set(s => s.Remaining, entity.Remaining)
                .Set(s => s.SavingsPercent, entity.SavingsPercent)
                .Set(s => s.MonthlyCategoryEntries, entity.MonthlyCategoryEntries)
                .Set(s => s.LastUpdatedDate, DateHelper.GetDateTimePH());

            return await Update(filter, update);
        }

        public async Task<Entity> GetById(string id)
        {
            var filter = Builders<Entity>.Filter.Eq(s => s.Id, id);

            return await FindOne(filter);
        }

        public async Task<Entity> GetByMonth(DateTime month)
        {
            var filter = Builders<Entity>.Filter.Eq(s => s.Month, month);

            return await FindOne(filter);
        }

        public async Task<bool> UpdateBasicDetails(string id, decimal totalFunds, string remarks)
        {
            var filter = Builders<Entity>.Filter.Eq(s => s.Id, id);

            var update = Builders<Entity>.Update
                .Set(s => s.TotalFunds, totalFunds)
                .Set(s => s.Remarks, remarks)
                .Set(s => s.LastUpdatedDate, DateHelper.GetDateTimePH());

            return await Update(filter, update);
        }
    }
}
