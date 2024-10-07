using System.ComponentModel.DataAnnotations;
using System.Linq;
using SalesAndPurchaseManagement.Data;

namespace SalesAndPurchaseManagement.DataAnnotationsModel
{

    public class UniquePhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (SAPManagementContext)validationContext.GetService(typeof(SAPManagementContext));
            var employeeId = (int)validationContext.ObjectType.GetProperty("EmployeeId").GetValue(validationContext.ObjectInstance);

            var phoneExists = context.Employees.Any(e => e.PhoneNumber == value.ToString() && e.EmployeeId != employeeId);

            if (phoneExists)
            {
                return new ValidationResult("Số điện thoại đã tồn tại trong hệ thống.");
            }

            return ValidationResult.Success;
        }
    }

}
