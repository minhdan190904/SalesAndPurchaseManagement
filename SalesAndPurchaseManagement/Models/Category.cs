using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Danh Mục")]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Danh Mục")]
        public string CategoryName { get; set; }

        [Display(Name = "Sản Phẩm")]
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
