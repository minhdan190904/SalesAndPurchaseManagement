using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Khách Hàng")]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Khách Hàng")]
        public string CustomerName { get; set; }

        [StringLength(250)]
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }

        [StringLength(15)]
        [Display(Name = "Số Điện Thoại")]
        public string PhoneNumber { get; set; }
    }
}
