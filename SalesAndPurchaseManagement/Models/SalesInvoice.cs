using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class SalesInvoice
    {
        [Key] // Đánh dấu SalesInvoiceId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int SalesInvoiceId { get; set; } // Số HDB - Primary key

        [Required] // Đánh dấu EmployeeId là thuộc tính bắt buộc
        public int EmployeeId { get; set; } // Mã NV - Foreign key

        [Required] // Đánh dấu InvoiceDate là thuộc tính bắt buộc
        public DateTime InvoiceDate { get; set; } // Ngày Bán

        [Required] // Đánh dấu CustomerId là thuộc tính bắt buộc
        public int CustomerId { get; set; } // Mã Khách - Foreign key

        [Required] // Đánh dấu TotalAmount là thuộc tính bắt buộc
        [Column(TypeName = "decimal(18, 2)")] // Xác định kiểu dữ liệu cho TotalAmount
        public decimal TotalAmount { get; set; } // Tổng Tiền

        // Khóa ngoại tới bảng Employee
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        // Khóa ngoại tới bảng Customer
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
