namespace Finance.API.Domain.ViewModel.Request
{
    public class UpsertEntryRequestVM
    {

        public required string Id { get; set; }
        public DateTime EntryDate { get; set; }
        public required decimal Amount { get; set; }
        public required string Description { get; set; }
        public required string Remarks { get; set; }
        public string? CategoryId { get; set; }
    }
}
