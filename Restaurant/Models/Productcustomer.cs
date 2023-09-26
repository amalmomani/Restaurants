﻿using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Productcustomer
{
    public decimal Id { get; set; }

    public decimal? ProductId { get; set; }

    public decimal? CustomerId { get; set; }

    public decimal? Quantity { get; set; }

    public DateTime? DateFrom { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Product? Product { get; set; }
}
