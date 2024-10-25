using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class CountryOfOrigin
    {
        [Key]
        [Display(Name = "Mã Quốc Gia")]
        public int CountryOfOriginId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Quốc Gia")]
        public string? CountryName { get; set; }

        [Display(Name = "Sản Phẩm")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
