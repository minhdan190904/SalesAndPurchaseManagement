using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int CategoryId { get; set; }

        [Required] 
        [StringLength(100)] // Đặt độ dài tối đa cho CategoryName
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
