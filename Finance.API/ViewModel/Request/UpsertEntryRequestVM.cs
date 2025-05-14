namespace Finance.API.ViewModel.Request
{
    public class UpsertEntryRequestVM
    {

        public string Id { get; set; }

        public required string Description { get; set; }

        public DateTime EntryDate { get; set; }

        public required string Remarks { get; set; }

        public string CategoryId { get; set; }
    }
}
