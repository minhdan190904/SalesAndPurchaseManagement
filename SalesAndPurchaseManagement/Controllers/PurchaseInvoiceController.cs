using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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
                .Include(i => i.Employee)
                .Include(i => i.Supplier)
                .Include(i => i.PurchaseInvoiceDetails)
                .ToListAsync();

            return View("Index", invoices);
        }

        // GET: PurchaseInvoice/Create
        public IActionResult Create()
        {
            PopulateViewBag();
            return View();
        }

        // POST: PurchaseInvoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseInvoice invoice, List<PurchaseInvoiceDetail> invoiceDetails)
        {
            if (!ModelState.IsValid)
            {
                PopulateViewBag();
                return View(invoice);
            }

            if (invoiceDetails == null || !invoiceDetails.Any())
            {
                ModelState.AddModelError(string.Empty, "Vui lòng thêm ít nhất một sản phẩm.");
                PopulateViewBag();
                return View(invoice);
            }

            try
            {
                // Assuming EmployeeId is obtained from the logged-in user's claims
                var employeeIdClaim = User.Claims.FirstOrDefault(c => c.Type == "EmployeeId");
                if (employeeIdClaim != null)
                {
                    invoice.EmployeeId = int.Parse(employeeIdClaim.Value);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không tìm thấy thông tin nhân viên.");
                    PopulateViewBag();
                    return View(invoice);
                }

                if (invoice.SupplierId <= 0)
                {
                    ModelState.AddModelError(nameof(invoice.SupplierId), "Nhà cung cấp không được để trống.");
                    PopulateViewBag();
                    return View(invoice);
                }

                invoice.InvoiceDate = DateTime.Now;
                _context.Add(invoice);
                await _context.SaveChangesAsync(); // Save to generate PurchaseInvoiceId

                long totalAmount = 0;

                foreach (var detail in invoiceDetails)
                {
                    if (detail.ProductId <= 0)
                    {
                        ModelState.AddModelError(string.Empty, "Mã sản phẩm không hợp lệ.");
                        PopulateViewBag();
                        return View(invoice);
                    }

                    var product = await _context.Products.FindAsync(detail.ProductId);
                    if (product == null)
                    {
                        ModelState.AddModelError(string.Empty, "Sản phẩm không tồn tại.");
                        PopulateViewBag();
                        return View(invoice);
                    }

                    product.Quantity += detail.Quantity;
                    product.PurchasePrice = detail.UnitPrice; // Update purchase price
                    _context.Update(product);

                    detail.PurchaseInvoiceId = invoice.PurchaseInvoiceId;
                    _context.Add(detail);

                    totalAmount += detail.TotalAmount;
                }

                invoice.TotalAmount = totalAmount;
                _context.Update(invoice);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Lỗi lưu dữ liệu: {ex.InnerException?.Message}");
                ModelState.AddModelError(string.Empty, "Lỗi khi lưu dữ liệu. Vui lòng kiểm tra thông tin và thử lại.");
                PopulateViewBag();
                return View(invoice);
            }
        }

        // GET: PurchaseInvoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.PurchaseInvoices
                .Include(i => i.PurchaseInvoiceDetails)
                    .ThenInclude(d => d.Product)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(i => i.PurchaseInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            PopulateViewBag();
            return View(invoice);
        }


        // POST: PurchaseInvoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PurchaseInvoice invoice, List<PurchaseInvoiceDetail> invoiceDetails)
        {
            if (id != invoice.PurchaseInvoiceId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                PopulateViewBag();
                return View(invoice);
            }

            if (invoiceDetails == null || !invoiceDetails.Any())
            {
                ModelState.AddModelError(string.Empty, "Vui lòng thêm ít nhất một sản phẩm.");
                PopulateViewBag();
                return View(invoice);
            }

            try
            {
                var existingInvoice = await _context.PurchaseInvoices
                    .Include(i => i.PurchaseInvoiceDetails)
                    .FirstOrDefaultAsync(i => i.PurchaseInvoiceId == id);

                if (existingInvoice == null)
                {
                    return NotFound();
                }

                // Update invoice properties
                existingInvoice.SupplierId = invoice.SupplierId;
                existingInvoice.InvoiceDate = invoice.InvoiceDate;

                // Revert product quantities from existing details
                foreach (var detail in existingInvoice.PurchaseInvoiceDetails)
                {
                    var product = await _context.Products.FindAsync(detail.ProductId);
                    if (product != null)
                    {
                        product.Quantity -= detail.Quantity;
                        if (product.Quantity < 0) product.Quantity = 0;
                        _context.Update(product);
                    }
                }

                // Remove existing invoice details
                _context.PurchaseInvoiceDetails.RemoveRange(existingInvoice.PurchaseInvoiceDetails);
                await _context.SaveChangesAsync();

                // Add new invoice details
                long totalAmount = 0;
                foreach (var detail in invoiceDetails)
                {
                    var product = await _context.Products.FindAsync(detail.ProductId);
                    if (product == null)
                    {
                        ModelState.AddModelError(string.Empty, $"Sản phẩm với ID {detail.ProductId} không tồn tại.");
                        PopulateViewBag();
                        return View(invoice);
                    }

                    product.Quantity += detail.Quantity;
                    product.PurchasePrice = detail.UnitPrice;
                    _context.Update(product);

                    detail.PurchaseInvoiceId = id;
                    _context.PurchaseInvoiceDetails.Add(detail);

                    totalAmount += detail.TotalAmount;
                }

                existingInvoice.TotalAmount = totalAmount;
                _context.Update(existingInvoice);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Lỗi khi lưu dữ liệu: {ex.InnerException?.Message}");
                ModelState.AddModelError(string.Empty, "Lỗi khi lưu dữ liệu. Vui lòng kiểm tra thông tin và thử lại.");
                PopulateViewBag();
                return View(invoice);
            }
        }

        // GET: PurchaseInvoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.PurchaseInvoices
                .Include(i => i.Employee)
                .Include(i => i.Supplier)
                .Include(i => i.PurchaseInvoiceDetails)
                    .ThenInclude(d => d.Product)
                        .ThenInclude(p => p.Manufacturer)
                .FirstOrDefaultAsync(i => i.PurchaseInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View("Details", invoice);
        }

        // GET: PurchaseInvoice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.PurchaseInvoices
                .Include(i => i.Supplier)
                .Include(i => i.Employee)
                .Include(i => i.PurchaseInvoiceDetails)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(i => i.PurchaseInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View("Delete", invoice);
        }

        // POST: PurchaseInvoice/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.PurchaseInvoices
                .Include(i => i.PurchaseInvoiceDetails)
                .FirstOrDefaultAsync(i => i.PurchaseInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            // Revert product quantities
            foreach (var detail in invoice.PurchaseInvoiceDetails)
            {
                var product = await _context.Products.FindAsync(detail.ProductId);
                if (product != null)
                {
                    product.Quantity -= detail.Quantity;
                    if (product.Quantity < 0) product.Quantity = 0;
                    _context.Update(product);
                }
            }

            // Remove invoice details and invoice
            _context.PurchaseInvoiceDetails.RemoveRange(invoice.PurchaseInvoiceDetails);
            _context.PurchaseInvoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Helper method to populate ViewBag data
        private void PopulateViewBag()
        {
            ViewBag.Suppliers = new SelectList(_context.Suppliers?.ToList() ?? new List<Supplier>(), "SupplierId", "SupplierName");
            ViewBag.Products = _context.Products?.Select(p => new SelectListItem
            {
                Value = p.ProductId.ToString(),
                Text = p.ProductName,
            }).ToList() ?? new List<SelectListItem>();
        }

        // AJAX action to get supplier information
        [HttpGet]
        public async Task<JsonResult> GetSupplierInfo(int supplierId)
        {
            var supplier = await _context.Suppliers
                .Where(s => s.SupplierId == supplierId)
                .Select(s => new
                {
                    s.SupplierId,
                    s.SupplierName,
                    s.Address,
                    s.PhoneNumber
                })
                .FirstOrDefaultAsync();

            return Json(supplier);
        }

        // AJAX action to get product information
        [HttpGet]
        public async Task<JsonResult> GetProductInfo(int productId)
        {
            var product = await _context.Products
                .Where(p => p.ProductId == productId)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.PurchasePrice
                })
                .FirstOrDefaultAsync();

            return Json(product);
        }

        private bool PurchaseInvoiceExists(int id)
        {
            return _context.PurchaseInvoices.Any(e => e.PurchaseInvoiceId == id);
        }
    }
}
