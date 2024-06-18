using System.Text.Json.Serialization;

namespace BaobabBackEndSerice.Models
{
    public class ChangeHistory
    {
        public int Id { get; set; }
        public string? ModifiedTable { get; set; }
        public int ModifiedRecordId { get; set; }
        public DateTime Date { get; set; }
        public string? ModifiedType { get; set; }
        public int? MarketingUserId { get; set; }
        public MarketingUser? MarketingUser { get; set; }
    }
}