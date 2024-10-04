using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Manufacturer
    {
        [Key] // Đánh dấu ManufacturerId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int ManufacturerId { get; set; }

        [Required] // Đánh dấu ManufacturerName là thuộc tính bắt buộc
        [StringLength(100)] // Đặt độ dài tối đa cho ManufacturerName
        public string ManufacturerName { get; set; }

        // Mối quan hệ một-nhiều với Product
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
