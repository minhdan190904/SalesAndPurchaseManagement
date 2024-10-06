using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAndPurchaseManagement.Data;
using SalesAndPurchaseManagement.Services;
using SalesAndPurchaseManagement.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SalesAndPurchaseManagement.Controllers
{
    public class AccessController : Controller
    {
        private readonly SAPManagementContext _context;
        private readonly IEmailService _emailService;
        private static string OTPCodeTemp = ""; // Thay đổi từ instance thành static

        public AccessController(SAPManagementContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Employee");
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
                    await _emailService.SendEmailAsync(employee.Email, "Mã OTP Đặt Lại Mật Khẩu", $"Mã OTP của bạn là: {OTPCodeTemp}");
                    return RedirectToAction("ResetPasswordConfirm", new { email = model.Email });
                }
                else
                {
                    ViewData["Message"] = "Email không tồn tại trong hệ thống.";
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
                    if (model.OtpCode == OTPCodeTemp) // Kiểm tra OTP
                    {
                        employee.Password = model.NewPassword; // Cập nhật mật khẩu
                        _context.Employees.Update(employee);
                        await _context.SaveChangesAsync();

                        ViewData["Message"] = "Mật khẩu đã được cập nhật thành công.";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewData["Message"] = "Mã OTP không hợp lệ.";
                    }
                }
                else
                {
                    ViewData["Message"] = "Email không tồn tại trong hệ thống.";
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
