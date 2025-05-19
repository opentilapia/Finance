namespace Finance.API.Common.Interface
{
    public interface IRowMapper<T>
    {
        T Map(string[] columns, int row);
    }
}
