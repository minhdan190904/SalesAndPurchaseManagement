using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Category
    {
        [Key] // Đánh dấu CategoryId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int CategoryId { get; set; }

        [Required] // Đánh dấu CategoryName là thuộc tính bắt buộc
        [StringLength(100)] // Đặt độ dài tối đa cho CategoryName
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
