using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenewApplication.Models;

public partial class Bill
{
    public int BillId { get; set; }
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    public decimal Total { get; set; }

    public decimal Paid { get; set; }

    public decimal Due { get; set; }

    public string? CPhone { get; set; }

    public DateTime BDate { get; set; }

    public decimal NetTotal { get; set; }

    public decimal? Discount { get; set; }

    public virtual ICollection<BillDet> BillDets { get; set; } = new List<BillDet>();

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
