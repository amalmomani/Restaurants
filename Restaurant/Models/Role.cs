using System;
using System.Collections.Generic;

namespace Restaurant.Models;

public partial class Role
{
    public decimal Id { get; set; }

    public string Rolename { get; set; } = null!;

    public virtual ICollection<Userlogin> Userlogins { get; set; } = new List<Userlogin>();
}
