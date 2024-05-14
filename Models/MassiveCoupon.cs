namespace BaobabBackEndSerice.Models
{
    public class MassiveCoupon
    {
        public int Id { get; set; }
        public string? MassiveCouponCode { get; set; }
        public int CouponId { get; set; }
        public string? UserEmail { get; set; }
        public string? PurchaseId { get; set; }
        public DateTime? RedemptionDate { get; set; }
        public float purchaseValue { get; set; }
    }
}

