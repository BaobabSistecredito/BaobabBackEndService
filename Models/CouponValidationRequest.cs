using System.ComponentModel.DataAnnotations;
namespace BaobabBackEndSerice.Models
{
    public class CouponValidationRequest
    {
        public string CouponCode { get; set; }
        public float PurchaseValue { get; set; }
    }
}