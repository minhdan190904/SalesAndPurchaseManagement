using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Nhà Cung Cấp")]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Nhà Cung Cấp")]
        public string SupplierName { get; set; }

        [StringLength(200)]
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }

        [StringLength(15)]
        [Display(Name = "Số Điện Thoại")]
        public string PhoneNumber { get; set; }

        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
    }
}
