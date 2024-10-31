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

                    int totalAmount = 0;

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
                        await _context.SaveChangesAsync();

                        totalAmount += detail.TotalAmount;

                        detail.PurchaseInvoiceId = invoice.PurchaseInvoiceId; // Gán ID hóa đơn cho chi tiết
                        _context.Add(detail);
                        await _context.SaveChangesAsync();
                    }

                    invoice.TotalAmount = totalAmount;

                    _context.Add(invoice);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(invoice);
                }
            }

            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Count > 0)
                {
                    Console.WriteLine($"{key}: {ModelState[key].Errors[0].ErrorMessage}");
                }
            }

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
    }
}
