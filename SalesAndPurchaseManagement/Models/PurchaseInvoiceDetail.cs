using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class PurchaseInvoiceDetail
    {
        [Key] // Đánh dấu PurchaseInvoiceDetailId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int PurchaseInvoiceDetailId { get; set; } // Primary key

        [Required] // Đánh dấu PurchaseInvoiceId là thuộc tính bắt buộc
        public int PurchaseInvoiceId { get; set; } // Foreign key, Số HDN

        [Required] // Đánh dấu ProductId là thuộc tính bắt buộc
        public int ProductId { get; set; } // Foreign key, Mã hàng

        [Required] // Đánh dấu Quantity là thuộc tính bắt buộc
        public int Quantity { get; set; } // Số lượng

        [Required] // Đánh dấu UnitPrice là thuộc tính bắt buộc
        [Column(TypeName = "decimal(18, 2)")] // Xác định kiểu dữ liệu cho UnitPrice
        public decimal UnitPrice { get; set; } // Đơn giá bán (giống như Product)

        [Required] // Đánh dấu Discount là thuộc tính bắt buộc
        [Column(TypeName = "decimal(5, 2)")] // Kiểu dữ liệu cho Discount
        public decimal Discount { get; set; } // Giảm giá (tính theo %)

        // Thành tiền = Số lượng * Đơn giá * (1 - Giảm giá)
        [NotMapped] // Không lưu thuộc tính này trong cơ sở dữ liệu
        public decimal TotalAmount => Quantity * UnitPrice * (1 - Discount / 100); // Thành tiền

        // Khóa ngoại tới bảng PurchaseInvoice
        [ForeignKey("PurchaseInvoiceId")]
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }

        // Khóa ngoại tới bảng Product
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
