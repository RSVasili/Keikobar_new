using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keikobar.Models;

public class Product : BaseEntity
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Range(0,int.MaxValue, ErrorMessage = "Price for 'Product' must be is possible")]
    public decimal Price { get; set; }
    public string Image { get; set; }
    
    [Display(Name = "Category Type")]
    public Guid CategoryId { get; set; }
    
    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }
}