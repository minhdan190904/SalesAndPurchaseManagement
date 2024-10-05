using Microsoft.AspNetCore.Authorization;
using SalesAndPurchaseManagement.Models.SalesAndPurchaseManagement.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace SalesAndPurchaseManagement.Models
{
    [Authorize]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Nhân Viên")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Tên nhân viên là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên nhân viên không được vượt quá 100 ký tự.")]
        [Display(Name = "Tên Nhân Viên")]
        public string EmployeeName { get; set; } 

        [Required(ErrorMessage = "Giới tính là bắt buộc.")]
        [Display(Name = "Giới Tính")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Ngày sinh bắt buộc phải nhập")]
        [Range(typeof(DateTime), "1/2/1950", "12/31/2006", ErrorMessage = "Ngày sinh phải trong khoảng từ 1963 đến 2005")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [Display(Name = "Số Điện Thoại")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải bao gồm 10 chữ số.")]
        public string PhoneNumber { get; set; }
        //

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự.")]
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; } 

        [Display(Name = "Ảnh")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Bạn nên thêm công việc trước.")]
        [Display(Name = "Công Việc")]
        public int JobId { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }  

        [Required(ErrorMessage = "Thông tin quản trị viên là bắt buộc.")]
        [Display(Name = "Quyền Admin")]
        public bool IsAdmin { get; set; } 

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [ForeignKey("JobId")]
        public virtual Job? Job { get; set; }
    }
}
