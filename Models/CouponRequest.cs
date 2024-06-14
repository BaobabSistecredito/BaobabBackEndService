using System;
using System.ComponentModel.DataAnnotations;

namespace BaobabBackEndSerice.Models
{
  public class CouponRequest
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public string StartDate { get; set; }
    public string ExpiryDate { get; set; }
    public float ValueDiscount { get; set; }
    public string TypeDiscount { get; set; }
    public int NumberOfAvailableUses { get; set; }
    public string TypeUsability { get; set; }
    public float MinPurchaseRange { get; set; }
    public float MaxPurchaseRange { get; set; }
    public string CouponCode { get; set; }
    public int CategoryId { get; set; }
    public int MarketingUserId { get; set; }
    public string? StatusCoupon { get; set; }
  }
}
