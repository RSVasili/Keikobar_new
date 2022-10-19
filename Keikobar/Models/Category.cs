using System.ComponentModel;

namespace Keikobar.Models;

public class Category : BaseEntity
{
    public string Name { get; set; }
    
    [DisplayName("Display Order")]
    public int DisplayOrder { get; set; }
}