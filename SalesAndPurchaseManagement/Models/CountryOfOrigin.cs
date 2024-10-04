using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class CountryOfOrigin
    {
        [Key] // Đánh dấu CountryOfOriginId là khóa chính
        public int CountryOfOriginId { get; set; }

        [Required] // Đánh dấu CountryName là thuộc tính bắt buộc
        [StringLength(100)] // Đặt độ dài tối đa cho CountryName
        public string CountryName { get; set; }

        // Mối quan hệ một-nhiều với Product
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
