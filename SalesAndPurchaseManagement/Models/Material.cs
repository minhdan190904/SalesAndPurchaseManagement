using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Material
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Chất Liệu")]
        public int MaterialId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Chất Liệu")]
        public string MaterialName { get; set; }

        [Display(Name = "Sản Phẩm")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
