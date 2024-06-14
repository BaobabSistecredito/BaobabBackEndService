namespace BaobabBackEndService.DTOs
{
  public class RedeemDTO
  {

    public string? PurchaseId { get; set; }
    public string? UserEmail { get; set; }
    public float PurchaseValue { get; set; }
    public string? CodeCoupon { get; set; }

  }
}