using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Supplier
    {
        [Key] // Đánh dấu SupplierId là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
        public int SupplierId { get; set; } // Primary key

        [Required] // Đánh dấu SupplierName là thuộc tính bắt buộc
        [StringLength(100)] // Đặt độ dài tối đa cho SupplierName
        public string SupplierName { get; set; }

        [StringLength(200)] // Đặt độ dài tối đa cho Address
        public string Address { get; set; }

        [StringLength(15)] // Đặt độ dài tối đa cho PhoneNumber
        public string PhoneNumber { get; set; }

        // Mối quan hệ một-nhiều với PurchaseInvoice
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; } = new List<PurchaseInvoice>();
    }
}
