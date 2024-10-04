using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Shape
    {
        [Key] // Đánh dấu ShapeId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int ShapeId { get; set; } // Primary key

        [Required] // Đánh dấu ShapeName là thuộc tính bắt buộc
        [StringLength(100)] // Đặt độ dài tối đa cho ShapeName
        public string ShapeName { get; set; }

        // Mối quan hệ một-nhiều với Product
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
