using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RenewApplication.Models;

namespace RenewApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly WebarenaContext _context;

        public ProductController(WebarenaContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            return View(await _context.Items.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.IId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IId,Itemname,Gen,Harddisk,Ram,Quantity,Purchprice,Sellprice")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IId,Itemname,Gen,Harddisk,Ram,Quantity,Purchprice,Sellprice")] Item item)
        {
            if (id != item.IId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.IId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .FirstOrDefaultAsync(m => m.IId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ItemReports ()
        {
            //// Get the path to the RDLC file
            //string rdlcFilePath = Path.Combine(_context., "Reports", "OrderReport.rdlc");

            //// Create a local report instance
            //LocalReport localReport = new LocalReport(rdlcFilePath);

            //// Get data from database
            //var orders = await _orderRepository.GetOrdersAsync(startDate, endDate);

            //// Transform data for the report
            //var reportData = TransformOrdersToReportData(orders);

            //// Convert list to DataTable
            //DataTable dt = ConvertListToDataTable(reportData);

            //// Add the DataTable as a data source to the report
            //localReport.AddDataSource("OrderDataSet", dt);

            //// Add parameters if needed
            //if (startDate.HasValue && endDate.HasValue)
            //{
            //    var parameters = new Dictionary<string, string>
            //        {
            //            { "StartDate", startDate.Value.ToString("yyyy-MM-dd") },
            //            { "EndDate", endDate.Value.ToString("yyyy-MM-dd") }
            //        };

            //    localReport.SetParameters(parameters);
            //}

            //// Render the report
            //var result = localReport.Execute(RenderType.Pdf, 1);

            //// Return the PDF
            //return File(result.MainStream, "application/pdf", "OrderReport.pdf");
            return View();
        }
        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.IId == id);
        }
    }
}
