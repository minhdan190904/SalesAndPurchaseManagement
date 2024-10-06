using System.ComponentModel.DataAnnotations;

namespace SalesAndPurchaseManagement.ViewModels
{
    public class ResetPasswordConfirmViewModel
    {
        [Required(ErrorMessage = "Mã OTP là bắt buộc.")]
        public string OtpCode { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu mới phải có ít nhất 6 ký tự.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Xác nhận mật khẩu mới là bắt buộc.")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; } 
    }
}
