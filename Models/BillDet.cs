using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RenewApplication.Models;

public partial class BillDet
{
    public int BdId { get; set; }

    public int IId { get; set; }

    public string? IName { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }

    public int BillId { get; set; }

    //navigation properites
    public virtual Bill? Bill { get; set; }
    [ForeignKey("IId")]
    public virtual Item? Item { get; set; }
}
