using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Color
    {
        [Key] // Đánh dấu ColorId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int ColorId { get; set; }

        [Required] // Đánh dấu ColorName là thuộc tính bắt buộc
        [StringLength(50)] // Đặt độ dài tối đa cho ColorName
        public string ColorName { get; set; }

        // Mối quan hệ một-nhiều với Product
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
