using System.ComponentModel.DataAnnotations;

namespace SalesAndPurchaseManagement.DataAnnotationsModel
{

    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if (password.Contains(" "))
            {
                return new ValidationResult("Mật khẩu không được chứa khoảng trắng.");
            }

            if (password.Length < 6)
            {
                return new ValidationResult("Mật khẩu phải có ít nhất 6 ký tự.");
            }

            return ValidationResult.Success;
        }
    }

}
