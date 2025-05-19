using Finance.API.Common.Interface;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Functions;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finance.API.Common
{
    public class XlsFileParser<T> : IFileParser<T>
    {
        private readonly IRowMapper<T> _mapper;

        public XlsFileParser(IRowMapper<T> mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<T>> Parse(IFormFile file)
        {
            List<T> result = new List<T>();

            using (Stream stream = file.OpenReadStream())
            {
                HSSFWorkbook workbook = new HSSFWorkbook(stream);
                ISheet sheet = workbook.GetSheetAt(0);

                for(int i = 0; i <= sheet.LastRowNum; i++)
                {
                    if (i == 0)
                        continue;

                    IRow row = sheet.GetRow(i);

                    var rowData = new List<string>();

                    List<string> columnList = new List<string>();

                    for(int j = 0; j < row.LastCellNum; j++)
                    {
                        ICell cell = row.GetCell(j);

                        if (cell == null)
                            continue;

                        if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell))
                        {
                            DateTime? date = cell.DateCellValue; 
                            columnList.Add(date.Value.ToString("yyyy-MM-dd"));
                        }
                        else
                        {
                            columnList.Add(cell.ToString());
                        }
                    }

                    var item = _mapper.Map(columnList.ToArray(), i);

                    if (item == null)
                        break;

                    result.Add(item);
                }
            }
            return result;
        }

    }
}
