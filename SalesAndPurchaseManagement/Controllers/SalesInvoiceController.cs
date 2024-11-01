using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using System;
using System.Collections.Generic; // Thêm namespace này cho List
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
            // Kiểm tra ModelState ban đầu
            if (!ModelState.IsValid)
            {
                // Ghi log lỗi để kiểm tra
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

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

                // Kiểm tra xem CustomerId có hợp lệ không
                if (invoice.CustomerId <= 0)
                {
                    ModelState.AddModelError(nameof(invoice.CustomerId), "Khách hàng không được để trống.");
                    return View(invoice);
                }

                // Kiểm tra dữ liệu chi tiết hóa đơn
                if (invoiceDetails == null || !invoiceDetails.Any())
                {
                    ModelState.AddModelError(nameof(invoiceDetails), "Vui lòng thêm ít nhất một sản phẩm.");
                    return View(invoice);
                }

                long totalAmount = 0; // Thay đổi kiểu dữ liệu thành long

                // Lưu hóa đơn đầu tiên
                _context.Add(invoice);
                await _context.SaveChangesAsync(); // Lưu hóa đơn để nhận SalesInvoiceId

                foreach (var detail in invoiceDetails)
                {
                    if (detail.ProductId <= 0)
                    {
                        ModelState.AddModelError(nameof(detail.ProductId), "Mã hàng không hợp lệ.");
                        return View(invoice);
                    }

                    var product = await _context.Products.FindAsync(detail.ProductId);
                    if (product == null)
                    {
                        ModelState.AddModelError(nameof(detail.ProductId), "Mã hàng không tồn tại.");
                        return View(invoice);
                    }

                    if (product.Quantity < detail.Quantity)
                    {
                        ModelState.AddModelError(nameof(detail.Quantity), "Số lượng không đủ.");
                        return View(invoice);
                    }

                    product.Quantity -= detail.Quantity; // Cập nhật số lượng hàng tồn kho
                    _context.Update(product);

                    detail.SalesInvoiceId = invoice.SalesInvoiceId; // Gán ID hóa đơn cho chi tiết
                    _context.Add(detail);
                    totalAmount += detail.TotalAmount; // Sử dụng thuộc tính TotalAmount đã được tính toán
                }

                invoice.TotalAmount = totalAmount; // Cập nhật tổng tiền cho hóa đơn

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
                return View(invoice);
            }

            // Nếu có lỗi trong ModelState, hãy trả về View với lỗi
            return View(invoice);
        }


        private void PopulateViewBag()
        {
            ViewBag.Customers = new SelectList(_context.Customers, "CustomerId", "CustomerName");
            ViewBag.Products = new SelectList(_context.Products, "ProductId", "ProductName");
        }

        // API để lấy thông tin khách hàng
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

        // API để lấy thông tin sản phẩm
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

    }
}
