using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class SalesInvoiceDetail
    {
        [Key] // Đánh dấu SalesInvoiceDetailId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int SalesInvoiceDetailId { get; set; } // Primary key

        [Required] // Đánh dấu SalesInvoiceId là thuộc tính bắt buộc
        public int SalesInvoiceId { get; set; } // Foreign key

        [Required] // Đánh dấu ProductId là thuộc tính bắt buộc
        public int ProductId { get; set; } // Foreign key

        [Required] // Đánh dấu Quantity là thuộc tính bắt buộc
        public int Quantity { get; set; } // Số Lượng

        [Required] // Đánh dấu UnitPrice là thuộc tính bắt buộc
        [Column(TypeName = "decimal(18, 2)")] // Xác định kiểu dữ liệu cho UnitPrice
        public decimal UnitPrice { get; set; } // Đơn Gía

        [Required] // Đánh dấu Discount là thuộc tính bắt buộc
        [Column(TypeName = "decimal(18, 2)")] // Xác định kiểu dữ liệu cho Discount
        public decimal Discount { get; set; } // Giảm Giá

        [Required] // Đánh dấu TotalPrice là thuộc tính bắt buộc
        [Column(TypeName = "decimal(18, 2)")] // Xác định kiểu dữ liệu cho TotalPrice
        public decimal TotalPrice { get; set; } // Thành Tiền

        // Khóa ngoại tới bảng SalesInvoice
        [ForeignKey("SalesInvoiceId")]
        public virtual SalesInvoice SalesInvoice { get; set; }

        // Khóa ngoại tới bảng Product
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
