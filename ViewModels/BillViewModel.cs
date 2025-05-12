using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using RenewApplication.Models;
namespace RenewApplication.ViewModels
{
    public class BillViewModel
    {
        public int BillId { get; set; }
        public DateTime BDate { get; set; }
        public int CustomerId { get; set; }
        public string? CPhone { get; set; }
        public decimal Total { get; set; }
        public decimal Paid { get; set; }
        public decimal Due { get; set; }
        public decimal? Discount { get; set; }
        public decimal NetTotal { get; set; }
        //public int IId { get; set; }
        //public string? IName { get; set; }
        //public int Quantity { get; set; }
        //public decimal UnitPrice { get; set; }
        //public decimal SubTotal { get; set; }

        public List<BillDetailViewModel> BillDetails { get; set; } = new List<BillDetailViewModel>();
    }

    public class BillDetailViewModel
    {
        public int BdId { get; set; }
        public int BillId { get; set; }
        public int IId { get; set; }
        public string? IName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        
    }
}
