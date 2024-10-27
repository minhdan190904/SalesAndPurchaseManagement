using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Services;
using SalesAndPurchaseManagement.ViewModels;
using SalesAndPurchaseManagement.Helpers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.Controllers
{
    public class AccessController : Controller
    {
        private readonly SAPManagementContext _context;
        private readonly IEmailService _emailService;
        private static string OTPCodeTemp = "";

        public AccessController(SAPManagementContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel modelLogin)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Employees
                    .SingleOrDefaultAsync(e => e.Email == modelLogin.Email && e.Password == modelLogin.Password);

                if (employee != null)
                {
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, employee.Email),
                        new Claim("IsAdmin", employee.IsAdmin.ToString()),
                        new Claim("EmployeeId", employee.EmployeeId.ToString())
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties
                    {
                        IsPersistent = modelLogin.KeepLoggedIn
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                    return RedirectToAction("Index", "Dashboard");
                }

                ViewData["ValidateMessage"] = AppDefaults.InvalidLoginMessage;
            }

            return View(modelLogin);
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendOtp(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Email == model.Email);
                if (employee != null)
                {
                    OTPCodeTemp = GenerateOtp();
                    await _emailService.SendEmailAsync(employee.Email, AppDefaults.ResetPasswordOtpMessage, AppDefaults.GetOtpMessage(OTPCodeTemp));
                    return RedirectToAction("ResetPasswordConfirm", new { email = model.Email });
                }
                else
                {
                    ViewData["Message"] = AppDefaults.EmailNotFoundMessage;
                }
            }
            return View("ResetPassword", model);
        }

        public IActionResult ResetPasswordConfirm(string email)
        {
            var model = new ResetPasswordConfirmViewModel
            {
                Email = email,
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasswordConfirm(ResetPasswordConfirmViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Email == model.Email);
                if (employee != null)
                {
                    if (model.OtpCode == OTPCodeTemp) 
                    {
                        employee.Password = model.NewPassword; 
                        _context.Employees.Update(employee);
                        await _context.SaveChangesAsync();

                        ViewData["Message"] = AppDefaults.PasswordUpdatedMessage;
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewData["Message"] = AppDefaults.InvalidOtpMessage;
                    }
                }
                else
                {
                    ViewData["Message"] = AppDefaults.EmailNotFoundMessage;
                }
            }
            return View(model);
        }

        private string GenerateOtp()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}
