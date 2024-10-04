using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly SAPManagementContext _context;

        public EmployeeController(SAPManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees.Include(e => e.Job).ToListAsync();
            return View(employees);
        }

        public IActionResult Create()
        {
            SetViewBagData();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem email đã tồn tại chưa
                if (_context.Employees.Any(e => e.Email == employee.Email))
                {
                    ModelState.AddModelError("Email", "Email này đã được sử dụng.");
                    SetViewBagData();
                    return View(employee);
                }

                // Lưu nhân viên
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            SetViewBagData();
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _context.Employees.Include(e => e.Job).FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            SetViewBagData(employee);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee, string password)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Cập nhật thông tin nhân viên
                    _context.Update(employee);

                    // Nếu mật khẩu không rỗng, cập nhật mật khẩu
                    if (!string.IsNullOrWhiteSpace(password))
                    {
                        employee.Password = password;
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Employees.Any(e => e.EmployeeId == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            SetViewBagData(employee);
            return View(employee);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.Include(e => e.Job).FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ViewDetail(int id)
        {
            var employee = _context.Employees
                .Include(e => e.Job)
                .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        private void SetViewBagData(Employee employee = null)
        {
            ViewBag.JobList = new SelectList(_context.Jobs.ToList(), "JobId", "JobTitle", employee?.JobId);
            ViewBag.GenderList = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text = "Nam", Value = "Male" },
                new SelectListItem { Text = "Nữ", Value = "Female" }
            }, "Value", "Text", employee?.Gender.ToString());
        }
    }
}
