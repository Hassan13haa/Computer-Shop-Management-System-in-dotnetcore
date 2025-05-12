using Microsoft.AspNetCore.Mvc.Rendering;


namespace RenewApplication.Models
{
    public class SellProduct
    {

        public Customer Customer { get; set; }=new Customer();
        public Bill Bills { get; set; } = new Bill();
        public List<BillDet> BillDet { get; set; } = new List<BillDet>();

        //public string CustomerName { get; set; }
        //public decimal Total { get; set; }

        //public decimal Paid { get; set; }

        //public decimal Due { get; set; }

        //public string? CPhone { get; set; }
        //public DateTime BDate { get; set; }
        //public Customer? Customer { get; set; }

        //public Bill? Bills { get; set; }
        //public BillDet? BillDets { get; set; }
        //public Bill Bill { get; set; } = new Bill();
        //public BillDet BillDets { get; set; } = new BillDet();

        //  public List<BillDet> BillDet { get; set; } = new List<BillDet>();
        // public List<SelectListItem> Customers { get; internal set; }
        //public List<SelectListItem> Customers { get; internal set; }
    }

    //public class BillDetailsItem
    //{
    //    public string? IName { get; set; }
    //    public int Quantity { get; set; }
    //    public decimal UnitPrice { get; set; }

    //}
}
