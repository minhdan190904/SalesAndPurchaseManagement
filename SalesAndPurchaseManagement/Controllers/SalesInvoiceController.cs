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

        public IActionResult Create()
        {
            PopulateViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalesInvoice invoice, List<SalesInvoiceDetail> invoiceDetails)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            try
            {
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

                if (invoiceDetails == null || !invoiceDetails.Any())
                {
                    ModelState.AddModelError(nameof(invoiceDetails), "Vui lòng thêm ít nhất một sản phẩm.");
                    PopulateViewBag();
                    return View(invoice);
                }

                invoice.InvoiceDate = DateTime.Now;

                long totalAmount = 0;
                _context.Add(invoice);
                await _context.SaveChangesAsync();

                foreach (var detail in invoiceDetails)
                {
                    if (detail.ProductId <= 0)
                    {
                        ModelState.AddModelError(nameof(detail.ProductId), "Mã hàng không hợp lệ.");
                        PopulateViewBag();
                        return View(invoice);
                    }

                    var product = await _context.Products.FindAsync(detail.ProductId);
                    if (product == null)
                    {
                        ModelState.AddModelError(nameof(detail.ProductId), "Mã hàng không tồn tại.");
                        PopulateViewBag();
                        return View(invoice);
                    }

                    if (product.Quantity < detail.Quantity)
                    {
                        ModelState.AddModelError(nameof(detail.Quantity), "Số lượng không đủ.");
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

            PopulateViewBag();
            return View(invoice);
        }

        private void PopulateViewBag()
        {
            ViewBag.Customers = new SelectList(_context.Customers?.ToList() ?? new List<Customer>(), "CustomerId", "CustomerName");
            ViewBag.Products = new SelectList(_context.Products?.ToList() ?? new List<Product>(), "ProductId", "ProductName");
        }

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

        public JsonResult GetProductInfo(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                return Json(new
                {
                    productId = product.ProductId,
                    productName = product.ProductName,
                    availableQuantity = product.Quantity,
                    sellingPrice = product.SellingPrice
                });
            }
            return Json(null);
        }

        // GET: SalesInvoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.SalesInvoices
                .Include(i => i.SalesInvoiceDetails)
                .FirstOrDefaultAsync(i => i.SalesInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            PopulateViewBag();
            return View(invoice);
        }

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
                // Handle model state errors
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                PopulateViewBag(); // Assuming you have a method to populate view data
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

                // Update properties
                existingInvoice.CustomerId = invoice.CustomerId;
                existingInvoice.InvoiceDate = invoice.InvoiceDate; // Make sure to update the date
                existingInvoice.TotalAmount = 0; // Reset total amount to recalculate

                // Process invoice details and update existingInvoice.SalesInvoiceDetails if necessary...

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error saving data: {ex.InnerException?.Message}");
                ModelState.AddModelError(string.Empty, "Error saving data. Please check your input and try again.");
                PopulateViewBag(); // Ensure this method populates your ViewBag
                return View(invoice);
            }
        }


        private bool SalesInvoiceExists(int id)
        {
            return _context.SalesInvoices.Any(e => e.SalesInvoiceId == id);
        }


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
                    .ThenInclude(p => p.Manufacturer)
                .FirstOrDefaultAsync(i => i.SalesInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View("Details", invoice);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Tải hóa đơn cùng thông tin khách hàng, chi tiết hóa đơn và sản phẩm liên quan
            var invoice = await _context.SalesInvoices
                .Include(i => i.Customer)
                .Include(i => i.SalesInvoiceDetails)
                    .ThenInclude(d => d.Product)
                .Include(i => i.Employee) // Bao gồm thông tin nhân viên bán hàng
                .FirstOrDefaultAsync(i => i.SalesInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View("Delete", invoice);
        }



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
    }
}
