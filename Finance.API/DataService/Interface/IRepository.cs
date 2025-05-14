namespace Finance.API.DataService.Interface
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();

        Task<T> GetById(string id);

        Task<bool> Insert(T entity);

        Task<bool> Update(T entity);
    }
}
