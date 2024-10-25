using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Manufacturer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Nhà Sản Xuất")]
        public int ManufacturerId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Nhà Sản Xuất")]
        public string ManufacturerName { get; set; }

        [Display(Name = "Sản Phẩm")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
