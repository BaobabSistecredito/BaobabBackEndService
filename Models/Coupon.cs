namespace BaobabBackEndSerice.Models
{
  public class Coupon
  {
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? CreationDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public float ValueDiscount { get; set; }
    public string? TypeDiscount { get; set; }
    public int NumberOfAvailableUses { get; set; }
    public string? TypeUsability { get; set; }
    public string? StatusCoupon { get; set; }
    public float MinPurchaseRange { get; set; }
    public float MaxPurchaseRange { get; set; }
    public string? CouponCode { get; set; }
    public int CategoryId { get; set; }
    public int MarketingUserId { get; set; }
  }
}