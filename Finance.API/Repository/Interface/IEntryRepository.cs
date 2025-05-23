﻿using Finance.API.Domain.Class;

namespace Finance.API.DataService.Interface
{
    public interface IEntryRepository
    {
        Task<List<Entry>> GetPaginated(string categoryId, int pageSize, DateTime lastEntryDate);
        Task<Entry> GetById(string id);
        Task<List<Entry>> GetByDateRange(DateTime startDate, DateTime endDate);
        Task<bool> Upsert(Entry entity);
        Task<bool> Delete(string id);
        Task<bool> BatchInsert(List<Entry> entities);
    }
}
