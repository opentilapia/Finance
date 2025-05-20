using Finance.API.Domain.Class;

namespace Finance.API.Domain.ViewModel
{
    public class EntryVM
    {
        public EntryVM(Entry entity)
        {
            Id = entity.Id.ToString();
            EntryDate = entity.EntryDate;
            Amount = entity.Amount;
            Remarks = entity.Remarks;
            CategoryId = entity.CategoryId.ToString();
        }

        public string Id { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Amount { get; set; }
        public string Remarks { get; set; }
        public string CategoryId { get; set; }
    }
}
