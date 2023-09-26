using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Category
{
    public decimal Id { get; set; }

    public string? Categoryname { get; set; }

    public string? Imagepath { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
