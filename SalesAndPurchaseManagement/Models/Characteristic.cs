using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Characteristic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Đặc Điểm")]
        public int CharacteristicId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Đặc Điểm")]
        public string CharacteristicName { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>(); 
    }
}
