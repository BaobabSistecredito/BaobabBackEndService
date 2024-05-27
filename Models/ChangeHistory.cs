namespace BaobabBackEndSerice.Models
{
    public class ChangeHistory
    {
        public int Id { get; set; }
        public string? ModifiedTable { get; set; }
        public int IdModifiedRecord { get; set; }
        public DateTime ChangeDate { get; set; }
        public int IdMarketingUser { get; set; }
    }   
}