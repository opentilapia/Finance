namespace Finance.API.ViewModel.Request
{
    public class UpsertCategoryRequestVM
    {

        public string? Id {get;set;}

        public required string CategoryName { get; set; }

        public required string ColorCoding { get; set; }
    }
}
