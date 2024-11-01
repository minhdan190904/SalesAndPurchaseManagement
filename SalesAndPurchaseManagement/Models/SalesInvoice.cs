using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class SalesInvoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Hóa Đơn Bán")]
        public int SalesInvoiceId { get; set; }

        [Required]
        [Display(Name = "Mã Nhân Viên")]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "Ngày Bán")]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [Display(Name = "Mã Khách Hàng")]
        public int CustomerId { get; set; }

        [Required]
        [Column(TypeName = "BIGINT")]
        [Display(Name = "Tổng Tiền")]
        public long TotalAmount { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<SalesInvoiceDetail> SalesInvoiceDetails { get; set; } = new List<SalesInvoiceDetail>();
    }
}
