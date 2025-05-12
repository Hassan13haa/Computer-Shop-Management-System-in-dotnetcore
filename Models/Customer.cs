using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenewApplication.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? Addres { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }
    [ForeignKey("Bill")]
    public int? BillId { get; set; }

    public virtual Bill? Bill { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } =new List<Bill>();
}
