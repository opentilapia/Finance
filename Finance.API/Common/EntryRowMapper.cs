using Finance.API.Common.Interface;
using Finance.API.Domain.Class;

namespace Finance.API.Common
{
    public class EntryRowMapper : IRowMapper<Entry>
    {
        const int COLUMN_COUNT = 3;
        public Entry Map(string[] columns, int row)
        {
            if(columns.Length < COLUMN_COUNT)
            {
                throw new ApplicationException($"Incorrect number of columns. Expected {COLUMN_COUNT}");
            }

            if (columns[0].IsNullOrEmpty() || 
                columns[1].IsNullOrEmpty() || 
                columns[2].IsNullOrEmpty())
            {
                return null;
            }

            if (!DateTime.TryParse(columns[0], out DateTime entryDate))
            {
                throw new ApplicationException($"Error at row {row}: Invalid date. Date: {columns[0]}");
            }

            if (!decimal.TryParse(columns[1], out decimal amount))
            {
                throw new ApplicationException($"Error at row {row}: Invalid amount. Amount: {columns[1]}");
            }

            return new Entry()
            {
                EntryDate = entryDate,
                Amount = amount,
                Remarks = columns[2],
                CreatedDate = DateHelper.GetDateTimePH()
            };
        }
    }
}
