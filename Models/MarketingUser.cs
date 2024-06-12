namespace BaobabBackEndSerice.Models
{
  public class MarketingUser
  {
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int EmployeeId { get; set; }
    public string? Email { get; set; }
    public List<Coupon>? Coupons { get; set; }
  }
}

