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
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Đơn Giá")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Giảm Giá")]
        public decimal Discount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Thành Tiền")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("SalesInvoiceId")]
        public virtual SalesInvoice SalesInvoice { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
