using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class SalesInvoiceDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Chi Tiết Hóa Đơn Bán")]
        public int SalesInvoiceDetailId { get; set; }

        [Required]
        [Display(Name = "Mã Hóa Đơn Bán")]
        public int SalesInvoiceId { get; set; }

        [Required]
        [Display(Name = "Mã Sản Phẩm")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Số Lượng")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "BIGINT")]
        [Display(Name = "Đơn Giá")]
        public long UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "INT")]
        [Display(Name = "Giảm Giá")]
        public int Discount { get; set; }

        [NotMapped]
        [Display(Name = "Thành Tiền")]
        public long TotalAmount => Quantity * UnitPrice * (100 - Discount) / 100;

        [ForeignKey("SalesInvoiceId")]
        public virtual SalesInvoice? SalesInvoice { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
