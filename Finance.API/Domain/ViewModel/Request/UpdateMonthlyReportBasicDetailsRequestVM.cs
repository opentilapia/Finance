namespace Finance.API.Domain.ViewModel.Request
{
    public class UpdateMonthlyReportBasicDetailsRequestVM
    {

        public required string Id { get; set; }
        public decimal TotalFunds { get; set; } 
        public string? Remarks { get; set; }
    }
}
