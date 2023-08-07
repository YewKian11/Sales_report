using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sales_Report.Areas.Identity.Data;
using Sales_Report.Data;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Linq.Expressions;
using Microsoft.VisualBasic;
using NuGet.Versioning;
using Microsoft.AspNetCore.Mvc.Formatters;
using Humanizer;
using System.Runtime.InteropServices;

namespace Sales_Report.Controllers
{
    public class SalesTransactionController : Controller
    {
        private readonly Sales_ReportContext _context;

        public SalesTransactionController(Sales_ReportContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //Should I create a list for better manage table ?

            //Or default way 
            var sales = _context.salesTransactions;
            return View(sales);
        }
        [HttpGet]
        public async Task<IActionResult> Create() 
        {
         return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionId", "SalesItem", "SalesAmount", "SalesDate", "UserId")]SalesTransaction sales) 
        {
            
            if (!ModelState.IsValid)
            {
               await _context.AddAsync(sales);
               await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));
            }

            return View(sales);
         }

        [HttpGet]
        [Route("SalesTransaction/Edit/{transactionId}")]
        public async Task<IActionResult> Edit(int transactionId) 
        {
            if (transactionId == null)
            {
              return NoContent();
            }
          SalesTransaction sales = _context.salesTransactions.FindAsync(transactionId).GetAwaiter().GetResult();
            if (sales ==null ) 
            {
             return NotFound();
            }
            return View(sales);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("TransactionId,SalesItem,SalesAmount,SalesDate,UserId")]SalesTransaction sales) 
        {
            if (!ModelState.IsValid) 
            {
           /*  _context.Entry(sales).State = EntityState.Modified;*/
           _context.salesTransactions.Update(sales);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); 
            }
            return View(sales);
        }

        [HttpGet]
        [Route("SalesTransaction/Delete/{transactionId}")]
        public async Task<IActionResult> Delete(int transactionId) 
        {
            if (transactionId == null) 
            {
                return NoContent();
            }

            SalesTransaction sales = _context.salesTransactions.FindAsync(transactionId).GetAwaiter().GetResult();
            if (sales == null)
            {
                return NotFound();
            }
            return View(sales);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int transactionId) 
        {
            if (transactionId == null) 
            {
                return NoContent();
            }
           SalesTransaction sales = await _context.salesTransactions.FindAsync(transactionId);
            if (sales != null)
            {
                _context.salesTransactions.Remove(sales);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index)); 
            }
            return View(transactionId);
        }
        [HttpGet]
        public async Task<IActionResult> YearlySales(string SelectedYear)
        {
            // Display all data and Ascending order by month 
            var sales = _context.salesTransactions
                        .OrderBy(e => e.SalesDate.Month);
            if (_context.salesTransactions == null) 
            {
                return Problem("Entity set 'Sales_Report Context is null'");
            }

            var year = _context.salesTransactions
                .Select(e => e.SalesDate.Year)
                .Distinct()
                .ToListAsync();



            var DataYearSales = new YearlySalesModel 
            {
              SalesYear = new SelectList(await year),
              SalesList =  await sales.ToListAsync()
            };

            return View(DataYearSales);
        }

        [HttpPost, ActionName("YearlySales")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Salesresult(string SelectedYear)
        {
            var sales = _context.salesTransactions;
            //Get Year Selection List 
            var year = _context.salesTransactions
                .Select(e => e.SalesDate.Year)
              .Distinct()
              .ToListAsync();

            //Get record 
            var getRecord =  _context.salesTransactions
                .Where(a => a.SalesDate.Year.ToString() == SelectedYear.ToString());

            //Get Default or all data when Value is Nulll/Blank
            if (String.IsNullOrEmpty(SelectedYear))
            {
                var NullModel = new YearlySalesModel
                {
                    SalesYear = new SelectList(await year),
                    SalesList = await sales.ToListAsync()

                };  return View(NullModel);

            }
            //Got Value return related data
            var DataNewModel = new YearlySalesModel
            { SalesYear = new SelectList(await year),
                SalesList = await getRecord.ToListAsync()
            
            };
          
            return View(DataNewModel);  
        }

        [HttpGet]
        public async Task<IActionResult> MonthlySales() 
        {
            var sales = _context.salesTransactions;

            //Get Year and Month data
            var year = await _context.salesTransactions
                .Select(e => e.SalesDate.Year )
                .Distinct()
                .ToListAsync();


            if (_context.salesTransactions == null)
            {
                return Problem("Entity set 'Sales_Report Context is null'");
            }

            var msm = new MonthlySalesModel
            {   byYear = new SelectList( year),
                SalesList = await sales.ToListAsync()
            };
            return View(msm);
        }
        public JsonResult getMonthByYear(string selectedYear) 
        {
            var month = _context.salesTransactions
                .Where(e => e.SalesDate.Year.ToString() == selectedYear)
                .Select(e => e.SalesDate.Month)
                .Distinct().ToList();

            return Json(month);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MonthlySales(string takeYear, string takeMonth) 
        {
            var sales = _context.salesTransactions;

            //Get Year and Month data
            var year = await _context.salesTransactions
                      .Select(e => e.SalesDate.Year)
                      .Distinct()
                      .ToListAsync();

            var getResult = _context.salesTransactions
                .Where(e => e.SalesDate.Year.ToString() == takeYear.ToString() &&
                            e.SalesDate.Month.ToString() == takeMonth.ToString());
            var msmupdate = new MonthlySalesModel
            {
                byYear = new SelectList(year),
                SalesList = await getResult.ToListAsync()
            };
            return View(msmupdate);
        
        }
     
    }

  
}
