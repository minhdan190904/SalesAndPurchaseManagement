using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class PurchaseInvoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Số HDN")]
        public int PurchaseInvoiceId { get; set; }

        [Required]
        [Display(Name = "Mã NV")]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "Mã NCC")]
        public int SupplierId { get; set; }

        [Required]
        [Display(Name = "Ngày Nhập")]
        public DateTime InvoiceDate { get; set; }

        [Required]
        [Column(TypeName = "BIGINT")]
        [Display(Name = "Tổng Tiền")]
        public long TotalAmount { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier? Supplier { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }
        public virtual ICollection<PurchaseInvoiceDetail> PurchaseInvoiceDetails { get; set; } = new List<PurchaseInvoiceDetail>();
    }
}
