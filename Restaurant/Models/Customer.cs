using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models;

public partial class Customer
{
    public decimal Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? Imagepath { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public virtual ICollection<Productcustomer> Productcustomers { get; set; } = new List<Productcustomer>();

    public virtual ICollection<Userlogin> Userlogins { get; set; } = new List<Userlogin>();
}
