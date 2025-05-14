using Finance.API.Model;

namespace Finance.API.ViewModel
{
    public class EntryVM
    {
        public EntryVM(Entry entity) 
        { 
            Id = entity.Id.ToString();
            Description = entity.Description;
            EntryDate = entity.EntryDate;
            Remarks = entity.Remarks;
            CategoryId = entity.CategoryId.ToString();
        }

        public string Id { get; set; }

        public string Description { get; set; }

        public DateTime EntryDate { get; set; }

        public string Remarks { get; set; }

        public string CategoryId { get; set; }
    }
}
