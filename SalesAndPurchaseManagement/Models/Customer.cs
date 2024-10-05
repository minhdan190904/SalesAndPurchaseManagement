using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required] 
        [StringLength(100)] 
        public string CustomerName { get; set; }

        [StringLength(250)] 
        public string Address { get; set; }

        [StringLength(15)] 
        public string PhoneNumber { get; set; }
    }
}
