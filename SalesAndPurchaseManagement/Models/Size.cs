using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Size
    {
        [Key] // Đánh dấu SizeId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int SizeId { get; set; } // Primary key

        [Required] // Đánh dấu SizeName là thuộc tính bắt buộc
        [StringLength(100)] // Đặt độ dài tối đa cho SizeName
        public string SizeName { get; set; }

        // Mối quan hệ một-nhiều với Product
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
