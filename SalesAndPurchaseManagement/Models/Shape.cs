using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Shape
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Hình Dạng")]
        public int ShapeId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Hình Dạng")]
        public string ShapeName { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
