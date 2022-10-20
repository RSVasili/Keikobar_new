using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keikobar.Models; 

public class Product : BaseEntity
{
     [Required]
     public string Name { get; set; }

     public string ShortDesc { get; set; }
     public string Description { get; set; }
     [Range(0.01, 100.00, ErrorMessage = "Price must be between 0.01 and 100.00")]
     // [Range(typeof(decimal), "minimum", "maximum")]
     public decimal Price { get; set; }
     public string Image { get; set; }
     [Display(Name = "Category Type")]
     public Guid CategoryId { get; set; }
     [ForeignKey("CategoryId")]
     public virtual Category Category { get; set; }
     [Display(Name = "Application Type")]
     public Guid ApplicationTypeId { get; set; }
     [ForeignKey("ApplicationTypeId")]
     public virtual ApplicationType ApplicationType { get; set; }
}