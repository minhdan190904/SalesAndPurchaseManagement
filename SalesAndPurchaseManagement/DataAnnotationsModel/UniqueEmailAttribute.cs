using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SalesAndPurchaseManagement.Data;

namespace SalesAndPurchaseManagement.DataAnnotationsModel
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;
            if (string.IsNullOrWhiteSpace(email))
            {
                return new ValidationResult("Email là bắt buộc.");
            }

            var employee = (SalesAndPurchaseManagement.Models.Employee)validationContext.ObjectInstance;
            int employeeId = employee.EmployeeId;

            var _context = validationContext.GetService(typeof(SAPManagementContext)) as SAPManagementContext;

            var existingEmployee = _context.Employees
                .FirstOrDefault(e => e.Email == email && e.EmployeeId != employeeId);

            if (existingEmployee != null)
            {
                return new ValidationResult("Email đã tồn tại trong hệ thống.");
            }

            return ValidationResult.Success;
        }
    }
}
