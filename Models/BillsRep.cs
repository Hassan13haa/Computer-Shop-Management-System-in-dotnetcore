using System;
using System.Collections.Generic;

namespace RenewApplication.Models;

public partial class BillsRep
{
    public int BillId { get; set; }

    public string? CustomerName { get; set; }

    public string? ItemName { get; set; }

    public int? Quantity { get; set; }

    public int? UnitPrice { get; set; }

    public int? SubTotal { get; set; }

    public int? Total { get; set; }

    public int? NetTotal { get; set; }

    public int? Discount { get; set; }

    public int? Paid { get; set; }

    public int? Due { get; set; }

    public DateOnly? BDate { get; set; }
}
