using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAndPurchaseManagement.Data;
using System.Linq;

namespace SalesAndPurchaseManagement.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly SAPManagementContext _context;

        public DashboardController(SAPManagementContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var latestEmployees = _context.Employees
                .OrderByDescending(e => e.EmployeeId)
                .Take(8)
                .ToList();

            ViewBag.TotalSalesInvoices = _context.SalesInvoices
                .Where(i => i.InvoiceDate.Date == DateTime.Today)
                .Count();

            ViewBag.TotalMonthIncome = _context.SalesInvoices
                .Where(i => i.InvoiceDate.Month == DateTime.Today.Month)
                .Sum(i => i.TotalAmount);

            ViewBag.TotalUsers = _context.Customers.Count();

            ViewBag.TotalProducts = _context.Products.Count();

            return View(latestEmployees);
        }

    }
}
