using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.Controllers
{
    public class PurchaseInvoiceController : Controller
    {
        private readonly SAPManagementContext _context;

        public PurchaseInvoiceController(SAPManagementContext context)
        {
            _context = context;
        }

        // GET: PurchaseInvoice
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.PurchaseInvoices
                .Include(p => p.Supplier)
                .Include(p => p.Employee)
                .ToListAsync();
            return View(invoices);
        }

        // GET: PurchaseInvoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var invoice = await _context.PurchaseInvoices
                .Include(p => p.Supplier)
                .Include(p => p.Employee)
                .Include(p => p.PurchaseInvoiceDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(m => m.PurchaseInvoiceId == id);

            if (invoice == null) return NotFound();

            return View(invoice);
        }

        // GET: PurchaseInvoice/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View();
        }

        // POST: PurchaseInvoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseInvoice invoice, List<PurchaseInvoiceDetail> details)
        {
            if (ModelState.IsValid)
            {
                invoice.InvoiceDate = DateTime.Now;

                _context.Add(invoice);
                await _context.SaveChangesAsync();

                foreach (var detail in details)
                {
                    detail.PurchaseInvoiceId = invoice.PurchaseInvoiceId;
                    _context.PurchaseInvoiceDetails.Add(detail);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", invoice.SupplierId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeName", invoice.EmployeeId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            return View(invoice);
        }
    }
}
