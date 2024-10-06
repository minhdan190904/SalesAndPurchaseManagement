using System.ComponentModel.DataAnnotations;

namespace SalesAndPurchaseManagement.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email không được để trống.")]
        public string Email { get; set; } // username unique

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool KeepLoggedIn { get; set; }
    }

}
