using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Authorization;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SalesAndPurchaseManagement.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly SAPManagementContext _context;
        private readonly ICompositeViewEngine _viewEngine;

        public DashboardController(SAPManagementContext context, ICompositeViewEngine viewEngine)
        {
            _context = context;
            _viewEngine = viewEngine;
        }

        public IActionResult Index()
        {
            // Fetch the latest 8 employees
            var latestEmployees = _context.Employees
                .OrderByDescending(e => e.EmployeeId)
                .Take(8)
                .ToList();

            // Count of sales invoices for today
            ViewBag.TotalSalesInvoices = _context.SalesInvoices
                .Count(i => i.InvoiceDate.Date == DateTime.Today);

            // Total income for the current month
            ViewBag.TotalMonthIncome = _context.SalesInvoices
                .Where(i => i.InvoiceDate.Month == DateTime.Today.Month)
                .Sum(i => (decimal?)i.TotalAmount) ?? 0;

            // Total number of users and products
            ViewBag.TotalUsers = _context.Customers.Count();
            ViewBag.TotalProducts = _context.Products.Count();

            // Fetch categories to avoid multiple database calls
            var categories = _context.Categories.ToList();

            // Data for Donut and Pie charts with CategoryName
            var totalProductsSold = _context.SalesInvoiceDetails
                .GroupBy(sid => sid.Product.CategoryId)
                .Select(g => new
                {
                    CategoryId = g.Key,
                    TotalSold = g.Sum(sid => sid.Quantity),
                    TotalRevenue = g.Sum(sid => sid.Quantity * sid.Product.SellingPrice),
                    CategoryName = GetCategoryName(g.Key, categories)
                })
                .ToList();

            // Pass Donut and Pie data to ViewBag
            ViewBag.DonutData = totalProductsSold.Select(x => new { x.CategoryId, x.CategoryName, x.TotalSold }).ToList();
            ViewBag.PieData = totalProductsSold.Select(x => new { x.CategoryId, x.CategoryName, x.TotalRevenue }).ToList();

            return View(latestEmployees);
        }

        private static string GetCategoryName(int categoryId, List<Category> categories)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            return category != null ? category.CategoryName : "Không xác định";
        }

        public IActionResult Statistics(DateTime? startDate, DateTime? endDate)
        {
            // Set default time range if not provided
            startDate ??= DateTime.Today.AddMonths(-1);
            endDate ??= DateTime.Today;

            // Fetch sales invoices within the date range
            var salesInvoices = _context.SalesInvoices
                .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate)
                .ToList();

            // Calculate total revenue within the date range
            var totalRevenue = salesInvoices.Sum(i => (decimal?)i.TotalAmount) ?? 0;

            // Pass data to View
            ViewBag.TotalRevenue = totalRevenue;
            ViewBag.StartDate = startDate.Value.ToString("MM/dd/yyyy hh:mm tt");
            ViewBag.EndDate = endDate.Value.ToString("MM/dd/yyyy hh:mm tt");

            return View(salesInvoices);
        }

        [HttpGet]
        public IActionResult GetSalesInvoiceData(DateTime startDate, DateTime endDate)
        {
            // Filter sales invoices by the selected date range
            var salesInvoices = _context.SalesInvoices
                .Where(i => i.InvoiceDate >= startDate && i.InvoiceDate <= endDate)
                .Include(i => i.Customer)
                .Include(i => i.Employee)
                .ToList();

            // Calculate total revenue
            var totalRevenue = salesInvoices.Sum(i => (decimal?)i.TotalAmount) ?? 0;

            // Render the partial view as a string
            var htmlTable = RenderRazorViewToString("_SalesInvoiceTable", salesInvoices);

            // Return JSON response
            return Json(new { htmlTable, totalRevenue });
        }

        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext).Wait();
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
