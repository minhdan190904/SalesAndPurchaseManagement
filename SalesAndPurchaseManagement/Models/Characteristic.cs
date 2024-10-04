namespace SalesAndPurchaseManagement.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace SalesAndPurchaseManagement.Models
    {
        public class Characteristic
        {
            [Key] // Đánh dấu CharacteristicId là khóa chính
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID tự động sinh
            public int CharacteristicId { get; set; } // Mã đặc điểm

            [Required] // Đánh dấu CharacteristicName là thuộc tính bắt buộc
            [StringLength(100)] // Đặt độ dài tối đa cho CharacteristicName
            public string CharacteristicName { get; set; } // Tên đặc điểm (ví dụ: xoay, gấp...)
        }
    }

}
