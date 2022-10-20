using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Keikobar.Models;

public class ApplicationType : BaseEntity
{
    [Required]
    public string Name { get; set; }
}