using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Feature
    {
        [Key] // Đánh dấu FeatureId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int FeatureId { get; set; }

        [Required] // Đánh dấu FeatureName là thuộc tính bắt buộc
        [StringLength(100)] // Đặt độ dài tối đa cho FeatureName
        public string FeatureName { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
