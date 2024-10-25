namespace SalesAndPurchaseManagement.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Characteristic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Mã Đặc Điểm")]
        public int CharacteristicId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Tên Đặc Điểm")]
        public string CharacteristicName { get; set; }
    }
}
