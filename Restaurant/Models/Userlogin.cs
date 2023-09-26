﻿using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Userlogin
{
    public decimal Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public decimal? RoleId { get; set; }

    public decimal? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Role? Role { get; set; }
}
