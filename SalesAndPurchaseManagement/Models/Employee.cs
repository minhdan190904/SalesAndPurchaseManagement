using Microsoft.AspNetCore.Authorization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1900-01-01", "2005-12-31", ErrorMessage = "Ngày sinh phải trong khoảng từ 01/01/1900 đến 12/31/2005.")]
        [Display(Name = "Ngày Sinh")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Số Điện Thoại")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải bao gồm 10 chữ số.")]
        public string PhoneNumber { get; set; }

        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự.")]
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }

        [Display(Name = "Công Việc")]
        public int JobId { get; set; }

        [ForeignKey("JobId")]
        public virtual Job? Job { get; set; }

        [Required(ErrorMessage = "Thông tin quản trị viên là bắt buộc.")]
        [Display(Name = "Quyền Admin")]
        public bool IsAdmin { get; set; }

        // Có thể thêm thuộc tính khác nếu cần
    }
}

//awhbdhjawdhawhjadhwdhjawdhawd
