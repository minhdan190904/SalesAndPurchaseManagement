using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.Controllers
{
    public class SalesInvoiceController : Controller
    {
        private readonly SAPManagementContext _context;

        public SalesInvoiceController(SAPManagementContext context)
        {
            _context = context;
        }

        // GET: SalesInvoice
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.SalesInvoices
                .Include(i => i.Employee)
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceDetails)
                .ToListAsync();

            return View(invoices);
        }

        // GET: SalesInvoice/Create
        public IActionResult Create()
        {
            PopulateViewBag();
            return View();
        }

        // POST: SalesInvoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesInvoice invoice, List<SalesInvoiceDetail> invoiceDetails)
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

                if (invoice.CustomerId <= 0)
                {
                    ModelState.AddModelError(nameof(invoice.CustomerId), "Khách hàng không được để trống.");
                    PopulateViewBag();
                    return View(invoice);
                }

                invoice.InvoiceDate = DateTime.Now;
                _context.Add(invoice);
                await _context.SaveChangesAsync(); // Save to generate SalesInvoiceId

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

                    if (product.Quantity < detail.Quantity)
                    {
                        ModelState.AddModelError(string.Empty, $"Số lượng không đủ cho sản phẩm {product.ProductName}.");
                        PopulateViewBag();
                        return View(invoice);
                    }

                    product.Quantity -= detail.Quantity;
                    _context.Update(product);

                    detail.SalesInvoiceId = invoice.SalesInvoiceId;
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


        // GET: SalesInvoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the invoice along with its details and related entities
            var invoice = await _context.SalesInvoices
                .Include(i => i.SalesInvoiceDetails)
                    .ThenInclude(d => d.Product)
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(i => i.SalesInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            PopulateViewBag();
            return View(invoice);
        }

        // POST: SalesInvoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SalesInvoice invoice, List<SalesInvoiceDetail> invoiceDetails)
        {
            if (id != invoice.SalesInvoiceId)
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
                var existingInvoice = await _context.SalesInvoices
                    .Include(i => i.SalesInvoiceDetails)
                    .FirstOrDefaultAsync(i => i.SalesInvoiceId == id);

                if (existingInvoice == null)
                {
                    return NotFound();
                }

                // Update invoice properties
                existingInvoice.CustomerId = invoice.CustomerId;
                existingInvoice.InvoiceDate = invoice.InvoiceDate;

                // Restore product quantities from existing details
                foreach (var detail in existingInvoice.SalesInvoiceDetails)
                {
                    var product = await _context.Products.FindAsync(detail.ProductId);
                    if (product != null)
                    {
                        product.Quantity += detail.Quantity;
                        _context.Update(product);
                    }
                }

                // Remove existing invoice details
                _context.SalesInvoiceDetails.RemoveRange(existingInvoice.SalesInvoiceDetails);
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

                    if (product.Quantity < detail.Quantity)
                    {
                        ModelState.AddModelError(string.Empty, $"Số lượng không đủ cho sản phẩm {product.ProductName}.");
                        PopulateViewBag();
                        return View(invoice);
                    }

                    product.Quantity -= detail.Quantity;
                    _context.Update(product);

                    detail.SalesInvoiceId = id;
                    _context.SalesInvoiceDetails.Add(detail);

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

        // GET: SalesInvoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.SalesInvoices
                .Include(i => i.Employee)
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceDetails)
                    .ThenInclude(d => d.Product)
                        .ThenInclude(p => p.Manufacturer) // Include Manufacturer
                                                          // If Product has Color as a navigation property, include it as well
                                                          // .ThenInclude(p => p.Color)
                .FirstOrDefaultAsync(i => i.SalesInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }


        // GET: SalesInvoice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceDetails)
                    .ThenInclude(d => d.Product)
                .Include(i => i.Employee)
                .FirstOrDefaultAsync(i => i.SalesInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: SalesInvoice/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int salesInvoiceId)
        {
            var invoice = await _context.SalesInvoices
                .Include(i => i.SalesInvoiceDetails)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(i => i.SalesInvoiceId == salesInvoiceId);

            if (invoice == null)
            {
                return NotFound();
            }

            // Restore product quantities
            foreach (var detail in invoice.SalesInvoiceDetails)
            {
                if (detail.Product != null)
                {
                    detail.Product.Quantity += detail.Quantity;
                    _context.Update(detail.Product);
                }
            }

            _context.SalesInvoiceDetails.RemoveRange(invoice.SalesInvoiceDetails);
            _context.SalesInvoices.Remove(invoice);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Helper method to populate ViewBag data
        private void PopulateViewBag()
        {
            ViewBag.Customers = new SelectList(_context.Customers?.ToList() ?? new List<Customer>(), "CustomerId", "CustomerName");
            ViewBag.Products = _context.Products?.Select(p => new SelectListItem
            {
                Value = p.ProductId.ToString(),
                Text = p.ProductName,
            }).ToList() ?? new List<SelectListItem>();
        }

        // AJAX action to get customer information
        [HttpGet]
        public async Task<JsonResult> GetCustomerInfo(int customerId)
        {
            var customer = await _context.Customers
                .Where(c => c.CustomerId == customerId)
                .Select(c => new
                {
                    c.CustomerId,
                    c.CustomerName,
                    c.Address,
                    c.PhoneNumber
                })
                .FirstOrDefaultAsync();

            return Json(customer);
        }

        // AJAX action to get product information
        [HttpGet]
        public async Task<JsonResult> GetProductInfo(int productId)
        {
            var product = await _context.Products
                .Where(p => p.ProductId == productId)
                .Select(p => new
                {
                    productId = p.ProductId,
                    productName = p.ProductName,
                    availableQuantity = p.Quantity,
                    sellingPrice = p.SellingPrice
                })
                .FirstOrDefaultAsync();

            return Json(product);
        }

        private bool SalesInvoiceExists(int id)
        {
            return _context.SalesInvoices.Any(e => e.SalesInvoiceId == id);
        }
    }
}
