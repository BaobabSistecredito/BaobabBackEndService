using System;
using System.ComponentModel.DataAnnotations;

namespace BaobabBackEndSerice.Models
{
  public class CategoryRequest
  {

    [Required(ErrorMessage = "El campo CategoryName es Obligatorio")]
    [StringLength(200, ErrorMessage = "El titulo no puede ser mayor a 200 caracteres")]
    public string? CategoryName { get; set; }
    [Required(ErrorMessage = "El campo Status es Obligatorio")]
    public string? Status { get; set; }
  }
}