using System.Text.Json.Serialization;

namespace BaobabBackEndSerice.Models
{
  public class Category
  {
    public int Id { get; set; }
    public string? CategoryName { get; set; }
    public string? Status { get; set; }
    [JsonIgnore]
    public List<Coupon>? Coupons { get; set; }
  }
}