using System;
using System.ComponentModel.DataAnnotations;

namespace BaobabBackEndSerice.Models
{
  public class CouponRequest
  {

    [Required(ErrorMessage = "El campo Title es requerido")]
    [StringLength(255, ErrorMessage = "el Titulo no puede ser mayor a 255 caracteres")]
    public string Title { get; set; }

    public string Description { get; set; }

    [Required(ErrorMessage = "El campo StartDate es requerido")]
    public string StartDate { get; set; }

    [Required(ErrorMessage = "El campo ExpiryDate es requerido")]
    public string ExpiryDate { get; set; }

    [Required(ErrorMessage = "El campo ValueDiscount es requerido")]
    [Range(0.01, float.MaxValue, ErrorMessage = "ValueDiscount  tiene que ser mayor a 0")]
    public float ValueDiscount { get; set; }

    [Required(ErrorMessage = "El campo TypeDiscount es requerido")]
    [StringLength(30, ErrorMessage = "TypeDiscount no puede ser mayor a 30 caracteres")]
    public string TypeDiscount { get; set; }

    [Required(ErrorMessage = "El campo NumberOfAvailableUses es requerido")]
    [Range(1, int.MaxValue, ErrorMessage = "NumberOfAvailableUses must be at least 1")]
    public int NumberOfAvailableUses { get; set; }

    [Required(ErrorMessage = "El campo TypeUsability es requerido")]
    [StringLength(50, ErrorMessage = "El campo TypeUsability no puede ser mayor a 50 caracteres")]
    public string TypeUsability { get; set; }

    [Required(ErrorMessage = "El campo MinPurchaseRange es requerido")]
    [Range(0, float.MaxValue, ErrorMessage = "El campo MinPurchaseRange tiene que ser igual o mayor a 0")]
    public float MinPurchaseRange { get; set; }

    [Required(ErrorMessage = "El campo MaxPurchaseRange es requerido")]
    [Range(0, float.MaxValue, ErrorMessage = "El campo MaxPurchaseRange  tiene que ser igual o mayor a 0")]
    public float MaxPurchaseRange { get; set; }

    [StringLength(255, ErrorMessage = "El campo CouponCode no puede ser mayor a 255 caracteres")]
    public string CouponCode { get; set; }

    [Required(ErrorMessage = "El campo CategoryId es requerido")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "El campo MarketingUserId es requerido")]
    public int MarketingUserId { get; set; }
  }
}
//falta agregar la opcion de coupon unico y repetir con el codeCoupon 