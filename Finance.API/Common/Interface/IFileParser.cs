namespace Finance.API.Common.Interface
{
    public interface IFileParser<T>
    {
        Task<List<T>> Parse(IFormFile file);
    }
}
