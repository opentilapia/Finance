using Finance.API.Domain.Enum;

namespace Finance.API.Domain.ViewModel.Request
{
    public class UpsertCategoryRequestVM
    {

        public string? Id { get; set; }
        public required string CategoryName { get; set; }
        public required CategoryActionEnum Action { get; set; }
        public required string ColorCoding { get; set; }
    }
}
