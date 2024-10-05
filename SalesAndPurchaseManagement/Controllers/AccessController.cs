using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.Controllers
{
    public class AccessController : Controller
    {
        private readonly SAPManagementContext _context;

        public AccessController(SAPManagementContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelLogin)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra thông tin đăng nhập trong cơ sở dữ liệu
                var employee = await _context.Employees
                    .SingleOrDefaultAsync(e => e.Email == modelLogin.Email && e.Password == modelLogin.Password);

                if (employee != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, employee.Email),
                        new Claim("IsAdmin", employee.IsAdmin.ToString())
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties
                    {
                        IsPersistent = modelLogin.KeepLoggedIn
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Employee"); 
                }

                ViewData["ValidateMessage"] = "Thông tin đăng nhập không chính xác.";
            }

            return View(modelLogin);
        }
    }
}
