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
                .Include(i => i.Employee) // Bao gồm thông tin nhân viên
                .Include(i => i.Supplier) // Bao gồm thông tin nhà cung cấp
                .Include(i => i.PurchaseInvoiceDetails) // Bao gồm chi tiết hóa đơn
                .ToListAsync();

            return View("Index", invoices);
        }

        public IActionResult Create()
        {
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
            ViewBag.Products = new SelectList(_context.Products, "ProductId", "ProductName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseInvoice invoice, List<PurchaseInvoiceDetail> invoiceDetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy EmployeeId từ Claims
                    var employeeIdClaim = User.Claims.FirstOrDefault(c => c.Type == "EmployeeId");
                    if (employeeIdClaim != null)
                    {
                        invoice.EmployeeId = int.Parse(employeeIdClaim.Value); // Gán EmployeeId cho hóa đơn
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Không tìm thấy thông tin nhân viên.");
                        return View(invoice);
                    }

                    // Kiểm tra xem SupplierId có hợp lệ không
                    if (invoice.SupplierId <= 0)
                    {
                        ModelState.AddModelError(nameof(invoice.SupplierId), "Nhà cung cấp không được để trống.");
                        ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
                        ViewBag.Products = new SelectList(_context.Products, "ProductId", "ProductName");
                        return View(invoice);
                    }

                    invoice.InvoiceDate = DateTime.Now;
                    long totalAmount = 0; // Thay đổi kiểu dữ liệu thành long

                    // Lưu hóa đơn đầu tiên
                    _context.Add(invoice);
                    await _context.SaveChangesAsync(); // Lưu hóa đơn để nhận PurchaseInvoiceId

                    foreach (var detail in invoiceDetails)
                    {
                        if (detail.ProductId <= 0)
                        {
                            ModelState.AddModelError(nameof(detail.ProductId), "Mã hàng không hợp lệ.");
                            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
                            ViewBag.Products = new SelectList(_context.Products, "ProductId", "ProductName");
                            return View(invoice);
                        }

                        var product = await _context.Products.FindAsync(detail.ProductId);
                        if (product == null)
                        {
                            ModelState.AddModelError(nameof(detail.ProductId), "Mã hàng không tồn tại.");
                            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
                            ViewBag.Products = new SelectList(_context.Products, "ProductId", "ProductName");
                            return View(invoice);
                        }

                        product.Quantity += detail.Quantity; // Cập nhật số lượng hàng tồn kho
                        product.PurchasePrice = detail.UnitPrice; // Cập nhật giá nhập mới
                        _context.Update(product);

                        detail.PurchaseInvoiceId = invoice.PurchaseInvoiceId; // Gán ID hóa đơn cho chi tiết
                        _context.Add(detail);
                        totalAmount += detail.TotalAmount; // Sử dụng thuộc tính TotalAmount đã được tính toán
                    }

                    invoice.TotalAmount = (int)totalAmount; // Cập nhật tổng tiền cho hóa đơn

                    // Cập nhật hóa đơn với tổng tiền
                    _context.Update(invoice);
                    await _context.SaveChangesAsync(); // Lưu các thay đổi cuối cùng

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    var innerException = ex.InnerException;
                    Console.WriteLine(innerException?.Message);
                    ModelState.AddModelError(string.Empty, "Lỗi khi lưu dữ liệu. Vui lòng kiểm tra thông tin và thử lại.");
                    ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", invoice.SupplierId);
                    ViewBag.Products = new SelectList(_context.Products, "ProductId", "ProductName");
                    return View(invoice);
                }
            }

            // Nếu có lỗi trong ModelState, hãy trả về View với lỗi
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", invoice.SupplierId);
            ViewBag.Products = new SelectList(_context.Products, "ProductId", "ProductName");
            return View(invoice);
        }

        // API để lấy thông tin nhà cung cấp
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

        // API để lấy thông tin sản phẩm
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Tải hóa đơn cùng thông tin nhà cung cấp, nhân viên và chi tiết hóa đơn
            var invoice = await _context.PurchaseInvoices
                .Include(i => i.Supplier) // Bao gồm thông tin nhà cung cấp
                .Include(i => i.Employee) // Bao gồm thông tin nhân viên
                .Include(i => i.PurchaseInvoiceDetails) // Bao gồm chi tiết hóa đơn
                    .ThenInclude(d => d.Product) // Bao gồm thông tin sản phẩm
                .FirstOrDefaultAsync(i => i.PurchaseInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View("Delete", invoice);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.PurchaseInvoices
                .Include(i => i.PurchaseInvoiceDetails) // Bao gồm chi tiết hóa đơn
                .FirstOrDefaultAsync(i => i.PurchaseInvoiceId == id);

            if (invoice == null)
            {
                return NotFound();
            }

            // Xóa từng chi tiết hóa đơn
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

            // Xóa hóa đơn
            _context.PurchaseInvoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
