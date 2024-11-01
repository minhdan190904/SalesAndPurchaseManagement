using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using System;
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
                .Sum(i => (decimal?)i.TotalAmount) ?? 0; // Handle potential null values

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
                    // Using the static method to get category name
                    CategoryName = GetCategoryName(g.Key, categories)
                })
                .ToList();

            // Pass Donut and Pie data to ViewBag
            ViewBag.DonutData = totalProductsSold.Select(x => new { x.CategoryId, x.CategoryName, x.TotalSold }).ToList();
            ViewBag.PieData = totalProductsSold.Select(x => new { x.CategoryId, x.CategoryName, x.TotalRevenue }).ToList();

            return View(latestEmployees);
        }

        // Change to static method
        private static string GetCategoryName(int categoryId, List<Category> categories)
        {
            var category = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            return category != null ? category.CategoryName : "Không xác định"; // Provide default value
        }
    }
}
