using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.Controllers
{
    [Authorize]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, IFormFile? imageFile)
        {
            // Kiểm tra mật khẩu không chứa khoảng trắng
            if (employee.Password.Contains(" "))
            {
                ModelState.AddModelError("Password", "Mật khẩu không được có khoảng trắng.");
                SetViewBagData(employee);
                return View(employee);
            }

            if (ModelState.IsValid)
            {
                // Xử lý ảnh nếu có
                if (imageFile != null && imageFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    employee.Image = imageFile.FileName;
                }


                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            SetViewBagData(employee);
            return View(employee);
        }
        


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee, IFormFile? imageFile, string oldImage)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (employee.Password.Contains(" "))
            {
                ModelState.AddModelError("Password", "Mật khẩu không được có khoảng trắng.");
                SetViewBagData(employee);
                return View(employee);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(oldImage))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", oldImage);
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                  
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(stream);
                        }
                        employee.Image = imageFile.FileName;
                    }
                    else
                    {
 
                        employee.Image = oldImage;
                    }

                    _context.Update(employee);
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
