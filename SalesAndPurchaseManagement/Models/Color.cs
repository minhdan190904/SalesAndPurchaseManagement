using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Color
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Màu")]
        public int ColorId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên Màu")]
        public string ColorName { get; set; }

        [Display(Name = "Sản Phẩm")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
