using System;
using System.ComponentModel.DataAnnotations;

namespace Keikobar.Models;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}