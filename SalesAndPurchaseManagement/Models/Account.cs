using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Account
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; } 

        [Required(ErrorMessage = "Tên tài khoản là bắt buộc.")]
        [StringLength(50)] 
        public string Username { get; set; } 

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [StringLength(100)]
        public string Password { get; set; } 


        [Required] 
        public int EmployeeId { get; set; } 

        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; } 
    }
}
