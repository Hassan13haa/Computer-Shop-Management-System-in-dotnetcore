using System;
using System.Collections.Generic;

namespace RenewApplication.Models;

public partial class Repair
{
    public int RepId { get; set; }

    public string? CustName { get; set; }

    public string? Item { get; set; }

    public string? Descp { get; set; }

    public int? RepCost { get; set; }

    public DateOnly? RepDate { get; set; }
}
