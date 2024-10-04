using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Material
    {
        [Key] // Đánh dấu MaterialId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int MaterialId { get; set; }

        [Required] // Đánh dấu MaterialName là thuộc tính bắt buộc
        [StringLength(100)] // Đặt độ dài tối đa cho MaterialName
        public string MaterialName { get; set; }

        // Mối quan hệ một-nhiều với Product
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
