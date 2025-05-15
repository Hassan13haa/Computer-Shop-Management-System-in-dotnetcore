using AspNetCore.Reporting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RenewApplication.DataSets;
using RenewApplication.Models;
using RenewApplication.ViewModels;
using System.Data;
using System.Threading.Tasks;
using System.Transactions;

namespace RenewApplication.Controllers
{
    public class SellController : Controller
    {
        private readonly WebarenaContext _context;
        private readonly IWebHostEnvironment _webHost;

        public SellController(WebarenaContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        // GET: SellController
        public async Task<ActionResult> Index()
        {
           var customer = await _context.Customers
                .Include(c=>c.Bills)
                .ThenInclude(b=>b.BillDets)
                .ToListAsync();
            //create a viewmodel
            var viewModel = new customerBillingViewModel
            {
                Customers = customer.Select(c => new CustomerBillSummary
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    Email = c.Email,
                    Phone = c.Phone,
                    Bills = c.Bills.Select(b => new BillSummary
                    {
                        BillId = b.BillId,
                        BDate = b.BDate,
                        Total = b.Total,
                        NetTotal = b.NetTotal,
                        Paid = b.Paid,
                        Due = b.Due,
                        Details = b.BillDets.Select(d => new BillDetail
                        {
                            IName = d.IName,
                            Quantity = d.Quantity,
                            UnitPrice = d.UnitPrice,
                            SubTotal = d.SubTotal
                        }).ToList()
                    }).ToList(),
                }).ToList()

            };
           
            return View(viewModel);
        }

        // GET: SellController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SellController/Create
        public ActionResult Create()
        {
            var customer = _context.Customers.Select(c => new SelectListItem
            {
                Value = c.CustomerId.ToString(),
                Text = c.CustomerName
            }).ToList();
            ViewBag.Customers = customer;
            
            //var viewModel = new CustomerViewModel
            //{
            //    Customers = customers,
            //    SelectedCustomerId = selectedCustomerId ?? 0
            //};

            //if (selectedCustomerId.HasValue)
            //{
            //    var selectedCustomer = _context.Customers.FirstOrDefault(c => c.CustomerId == selectedCustomerId);
            //    if (selectedCustomer != null)
            //    {
            //        viewModel.PhoneNumber = selectedCustomer.PhoneNumber;
            //    }
            //}

            //var viewModel = new SellProduct
            //{

            //    Customer = new Customer(),
            //    //Customer = new Customer(),
            //    Bills =new Bill(),
            //    //Bill = new Bill(),
            //    BillDets= new BillDet(),
            //   // BillDet = new List<BillDet>{new BillDet()}
            //    //BillDets =new BillDet()
            //};
            //ViewBag.Customers = _context.Customers.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult GetCustomrPhone(string name)
        {
            var cuname = name;
            var ctname = _context.Customers
                .Where(c => c.CustomerName == cuname)
                .Select(c => c.Phone)
                .FirstOrDefault();

            return Json(new { success=true});
        }
        //public JsonResult GetPhoneNo(int customerId)
        //{
        //    return Json(_context.Customers.Where(c => c.CustomerId == customerId).Select(p => p.Phone).FirstOrDefault()

        //}

        //public async Task<IActionResult> GetPhoneNumber(int customerId)
        //{
        //    var customer = await _context.Customers
        //        .Where(c => c.CustomerId == customerId)
        //        .Select(c =>c.Phone)
        //        .FirstOrDefaultAsync();

        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return Json(new {customer});
        //}

        // POST: SellController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BillViewModel viewModel)
        {

            var customerExists = _context.Customers.Any(c => c.CustomerId == viewModel.CustomerId);

            if (!customerExists)
            {
                ModelState.AddModelError("CustomerId", "The selected customer does not exist.");
                ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            }
            
            //var itemExists = _context.Items.Any(i => i.IId == viewModel.BillDetails);
            //if (!itemExists)
            //{
            //    ModelState.AddModelError("IId", "The selected item does not exist.");
            //}

            var bill = new Bill
            {
                //BillId = viewModel.BillId,
                BDate = viewModel.BDate,
                CustomerId = viewModel.CustomerId,
                CPhone = viewModel.CPhone,
                Total = viewModel.Total,
                Paid = viewModel.Paid,
                Due = viewModel.Due,
                NetTotal = viewModel.NetTotal,
                Discount = viewModel.Discount,

            };
            _context.Bills.Add(bill);
            _context.SaveChanges();
            foreach(var detail in viewModel.BillDetails)
            {
                var billDetail = new BillDet
                {
                    BillId = bill.BillId, // Use the generated BillId
                    IId = detail.IId,
                    IName = detail.IName,
                    Quantity = detail.Quantity,
                    UnitPrice = detail.UnitPrice,
                    SubTotal = detail.SubTotal
                };
                _context.BillDets.Add(billDetail);

            }

            _context.SaveChanges();
            return View();
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: SellController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            var bill = await _context.Bills
           .Include(b => b.BillDets)
           .FirstOrDefaultAsync(b => b.BillId == id);

            if (bill == null)
            {
                return NotFound();
            }

            // Create the view model
            var viewModel = new BillViewModel
            {
                BillId = bill.BillId,
                BDate = bill.BDate,
                CustomerId = bill.CustomerId,
                CPhone = bill.CPhone,
                Total = bill.Total,
                Discount = bill.Discount,
                NetTotal = bill.NetTotal,
                Paid = bill.Paid,
                Due = bill.Due,
                BillDetails = bill.BillDets.Select(bd => new BillDetailViewModel
                {
                    BdId = bd.BdId,
                    IId = bd.IId,
                    IName = bd.IName,
                    Quantity = bd.Quantity,
                    UnitPrice = bd.UnitPrice,
                    SubTotal = bd.SubTotal
                }).ToList()
            };
            // Populate dropdown for customers
            //ViewBag.Customers = new SelectList(_context.Customers, "Id", "Name");
            return View(viewModel);
        }

        // POST: SellController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BillViewModel viewModel)
        {
            // Get the existing bill without including details
            var bill = _context.Bills.Find(id);
            if (bill == null)
            {
                return NotFound();
            }

            // Update bill properties
            bill.BDate = viewModel.BDate;
            bill.CustomerId = viewModel.CustomerId; // This should now be valid
            bill.CPhone = viewModel.CPhone;
            bill.Total = viewModel.Total;
            bill.Discount = viewModel.Discount;
            bill.NetTotal = viewModel.NetTotal;
            bill.Paid = viewModel.Paid;
            bill.Due = viewModel.Due;

            // Save the bill changes first
            _context.SaveChanges();

            // Delete existing bill details in a separate step
            var existingDetails = _context.BillDets.Where(bd => bd.BillId == id).ToList();
            if (existingDetails.Any())
            {
                _context.BillDets.RemoveRange(existingDetails);
                _context.SaveChanges();
            }

            // Add new bill details
            if (viewModel.BillDetails != null && viewModel.BillDetails.Any())
            {
                foreach (var detail in viewModel.BillDetails)
                {
                    // Validate that detail values are not null or empty
                    if (detail.IId <= 0 ||
                        string.IsNullOrEmpty(detail.IName) ||
                        detail.Quantity <= 0 ||
                        detail.UnitPrice <= 0 ||
                        detail.SubTotal <= 0)
                    {
                        continue; // Skip invalid details
                    }

                    var billDetail = new BillDet
                    {
                        BillId = bill.BillId,
                        IId = detail.IId,
                        IName = detail.IName,
                        Quantity = detail.Quantity,
                        UnitPrice = detail.UnitPrice,
                        SubTotal = detail.SubTotal
                    };
                    _context.BillDets.Add(billDetail);
                }
                _context.SaveChanges();
            }

           
            return RedirectToAction(nameof(Index));


        }

        // GET: SellController/Delete/5
        public ActionResult Delete(int id)
        {
            var bill = _context.Bills.FirstOrDefault(m => m.BillId == id);
            return View(bill);
        }

        // POST: SellController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            //try
            //{
                var bill = _context.Bills.Find(id);
                _context.Bills.Remove(bill);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        public ActionResult Invoice()
        {
            return View();
        }

        //public FileContentResult Print()
        //{
        //    string format = "PDF";
        //    string extension = "pdf";
        //    string mimeType = "application/pdf";

        //    string reportpath =$"{this._webHost.WebRootPath}\\Reports\\BillReport.rdlc";
        //    var localreport = new LocalReport(reportpath);
        //    localreport.AddDataSource("Dsbill");
            
        //    return File();
        //}

        
    }
}
