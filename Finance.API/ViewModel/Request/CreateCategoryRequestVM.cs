namespace Finance.API.ViewModel.Request
{
    public class CreateCategoryRequestVM
    {
        public required string CategoryName { get; set; }

        public required string ColorCoding { get; set; }
    }
}
