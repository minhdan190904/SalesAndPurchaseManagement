using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Customer
    {
        [Key] // Đánh dấu CustomerId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int CustomerId { get; set; }

        [Required] // Đánh dấu CustomerName là thuộc tính bắt buộc
        [StringLength(100)] // Đặt độ dài tối đa cho CustomerName
        public string CustomerName { get; set; }

        [StringLength(250)] // Đặt độ dài tối đa cho Address
        public string Address { get; set; }

        [StringLength(15)] // Đặt độ dài tối đa cho PhoneNumber
        public string PhoneNumber { get; set; }
    }
}
