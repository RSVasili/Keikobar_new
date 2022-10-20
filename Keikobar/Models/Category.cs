using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Keikobar.Models;

public class Category : BaseEntity
{
    [Required]
    public string Name { get; set; }
    [DisplayName("Display Order")]
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Min Value must be 1")]
    public int DisplayOrder { get; set; }
}