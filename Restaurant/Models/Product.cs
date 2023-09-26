using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Product
{
    public decimal Id { get; set; }

    public string? Name { get; set; }

    public decimal? Sale { get; set; }

    public decimal? Price { get; set; }

    public decimal? Categoryid { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Productcustomer> Productcustomers { get; set; } = new List<Productcustomer>();
}
