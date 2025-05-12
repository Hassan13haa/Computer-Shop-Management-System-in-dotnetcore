using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using RenewApplication.Models;
namespace RenewApplication.ViewModels

{
    public class customerBillingViewModel
    {
        public List<CustomerBillSummary> Customers { get; set; }
    }

    public class CustomerBillSummary
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = null!;

        public string? Addres { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public List<BillSummary> Bills { get; set; }
        public decimal TotalBilled { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalOutstanding => TotalBilled - TotalPaid;

    }

    public class BillSummary
    {
        public int BillId { get; set; }
        public DateTime BDate { get; set; }
        public decimal Total { get; set; }
        public decimal NetTotal { get; set; }
        public decimal? Discount { get; set; }
        public decimal Paid { get; set; }
        public decimal Due { get; set; }
        public List<BillDetail> Details { get; set; } 


    }

    public class BillDetail
    {

        public string? IName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
