using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class PurchaseInvoiceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Chi Tiết Hóa Đơn Nhập")]
        public int PurchaseInvoiceDetailId { get; set; }

        [Required]
        [Display(Name = "Số HDN")]
        public int PurchaseInvoiceId { get; set; }

        [Required]
        [Display(Name = "Mã Hàng")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Số Lượng")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Đơn Giá")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        [Display(Name = "Giảm Giá")]
        public decimal Discount { get; set; }

        [NotMapped]
        [Display(Name = "Thành Tiền")]
        public decimal TotalAmount => Quantity * UnitPrice * (1 - Discount / 100);

        [ForeignKey("PurchaseInvoiceId")]
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
