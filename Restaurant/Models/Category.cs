using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models;

public partial class Category
{
    public decimal Id { get; set; }

    public string? Categoryname { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public string? Imagepath { get; set; }
    public string? Test { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
