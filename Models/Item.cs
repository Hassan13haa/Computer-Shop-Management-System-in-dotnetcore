using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RenewApplication.Models;

public partial class Item
{
    [Key]
    public int IId { get; set; }

    public string Itemname { get; set; } = null!;

    public string? Gen { get; set; }

    public string? Harddisk { get; set; }

    public string? Ram { get; set; }

    public int Quantity { get; set; }

    public decimal Purchprice { get; set; }

    public decimal Sellprice { get; set; }

    //navigation property
    public ICollection<BillDet> BillDets { get; set; } = new List<BillDet>();
}
