using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class PurchaseInvoice
    {
        [Key] // Đánh dấu PurchaseInvoiceId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int PurchaseInvoiceId { get; set; } // Số HDN

        [Required] // Đánh dấu EmployeeId là thuộc tính bắt buộc
        public int EmployeeId { get; set; } // Mã NV (Foreign key)

        [Required] // Đánh dấu SupplierId là thuộc tính bắt buộc
        public int SupplierId { get; set; } // Mã NCC (Foreign key)

        [Required] // Đánh dấu InvoiceDate là thuộc tính bắt buộc
        public DateTime InvoiceDate { get; set; } // Ngày Nhập

        [Required] // Đánh dấu TotalAmount là thuộc tính bắt buộc
        [Column(TypeName = "decimal(18, 2)")] // Xác định kiểu dữ liệu cho TotalAmount
        public decimal TotalAmount { get; set; } // Tổng Tiền

        // Khóa ngoại tới bảng Supplier
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        // Khóa ngoại tới bảng Employee
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
