using Finance.API.Common.Interface;
using System.IO;

namespace Finance.API.Common
{
    public class CsvFileParser<T> : IFileParser<T>
    {
        private readonly IRowMapper<T> _mapper;

        public CsvFileParser(IRowMapper<T> mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<T>> Parse(IFormFile file)
        {
            List<T> result = new List<T>();

            int row = 0;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();

                    if (row == 0 || line.IsNullOrEmpty())
                    {
                        row++;
                        continue;
                    }

                    var columns = line.Split(',');
                    var item = _mapper.Map(columns, row);

                    result.Add(item);

                    row++;
                }
            }
            return result;
        }

    }
}
