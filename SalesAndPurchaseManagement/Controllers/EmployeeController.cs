using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Helpers;
using SalesAndPurchaseManagement.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.Controllers
{
    [Authorize(Policy = "AdminOnly")]
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
            var employee = new Employee
            {
                Image = AppDefaults.DefaultImageFile
            };

            SetViewBagData();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), AppDefaults.DefaultImageFolder, imageFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    employee.Image = imageFile.FileName;
                }
                else
                {
                    employee.Image = AppDefaults.DefaultImageFile;
                }

                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            employee.Image = AppDefaults.DefaultImageFile;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee, IFormFile? imageFile, string oldImage)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value ?? "0");
                    bool isSelfUpdate = (id == currentUserId);

                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), AppDefaults.DefaultImageFolder, imageFile.FileName);
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

                    if (isSelfUpdate && !employee.IsAdmin)
                    {
                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return RedirectToAction("Login", "Access");
                    }
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

            employee.Image = oldImage;
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
            var currentUserEmail = User.Identity.Name;
            var employeeToDelete = await _context.Employees.FindAsync(id);

            if (employeeToDelete == null)
            {
                return NotFound();
            }

            if (employeeToDelete.Email == currentUserEmail)
            {
                TempData["ErrorMessage"] = AppDefaults.CannotDeleteSelfMessage;
                return View("Delete", employeeToDelete);
            }

            _context.Employees.Remove(employeeToDelete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ViewDetail(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Job)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);

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
