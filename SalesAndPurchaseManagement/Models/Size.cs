using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Size
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Kích Cỡ")]
        public int SizeId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Kích Cỡ")]
        public string SizeName { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
