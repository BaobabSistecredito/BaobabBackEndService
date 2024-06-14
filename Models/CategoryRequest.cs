using System;
using System.ComponentModel.DataAnnotations;

namespace BaobabBackEndSerice.Models
{
  public class CategoryRequest
  {
    public string? CategoryName { get; set; }
    public string? Status { get; set; }
  }
}