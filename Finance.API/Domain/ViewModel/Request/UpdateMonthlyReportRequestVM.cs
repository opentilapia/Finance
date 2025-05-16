namespace Finance.API.Domain.ViewModel.Request
{
    public class UpdateMonthlyReportRequestVM
    {

        public required string Id { get; set; }
        public string? Remarks { get; set; }
    }
}
