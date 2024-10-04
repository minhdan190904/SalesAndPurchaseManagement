using SalesAndPurchaseManagement.Models.SalesAndPurchaseManagement.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesAndPurchaseManagement.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; } // Primary key

        [Required] // Đánh dấu ProductName là thuộc tính bắt buộc
        [StringLength(100)] // Đặt độ dài tối đa cho ProductName
        public string ProductName { get; set; }

        [Required] // Đánh dấu SizeId là thuộc tính bắt buộc
        public int SizeId { get; set; } // Foreign key

        [Required] // Đánh dấu CategoryId là thuộc tính bắt buộc
        public int CategoryId { get; set; } // Foreign key

        [Required] // Đánh dấu ShapeId là thuộc tính bắt buộc
        public int ShapeId { get; set; } // Foreign key

        [Required] // Đánh dấu MaterialId là thuộc tính bắt buộc
        public int MaterialId { get; set; } // Foreign key

        [Required] // Đánh dấu CountryOfOriginId là thuộc tính bắt buộc
        public int CountryOfOriginId { get; set; } // Foreign key

        [Required] // Đánh dấu ColorId là thuộc tính bắt buộc
        public int ColorId { get; set; } // Foreign key

        [Required] // Đánh dấu ManufacturerId là thuộc tính bắt buộc
        public int ManufacturerId { get; set; } // Foreign key

        [Required] // Đánh dấu FeatureId là thuộc tính bắt buộc
        public int FeatureId { get; set; } // Foreign key to Feature

        [Required] // Đánh dấu CharacteristicId là thuộc tính bắt buộc
        public int CharacteristicId { get; set; } // Foreign key for characteristics

        [Required] // Đánh dấu Quantity là thuộc tính bắt buộc
        public int Quantity { get; set; }

        [Required] // Đánh dấu PurchasePrice là thuộc tính bắt buộc
        [Column(TypeName = "decimal(18, 2)")] // Xác định kiểu dữ liệu cho PurchasePrice
        public decimal PurchasePrice { get; set; }

        [Required] // Đánh dấu SellingPrice là thuộc tính bắt buộc
        [Column(TypeName = "decimal(18, 2)")] // Xác định kiểu dữ liệu cho SellingPrice
        public decimal SellingPrice { get; set; }

        [Required] // Đánh dấu WarrantyPeriod là thuộc tính bắt buộc
        public int WarrantyPeriod { get; set; } // Thời gian bảo hành theo tháng

        // Thuộc tính lưu đường dẫn đến ảnh
        public string Image { get; set; }

        // Ghi chú cho sản phẩm
        [StringLength(500)] // Đặt độ dài tối đa cho Notes
        public string Notes { get; set; }

        // Navigation properties
        [ForeignKey("SizeId")]
        public virtual Size Size { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("ShapeId")]
        public virtual Shape Shape { get; set; }

        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; }

        [ForeignKey("CountryOfOriginId")]
        public virtual CountryOfOrigin Country { get; set; }

        [ForeignKey("ColorId")]
        public virtual Color Color { get; set; }

        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }

        [ForeignKey("FeatureId")]
        public virtual Feature Feature { get; set; } // Navigation property to Feature

        [ForeignKey("CharacteristicId")]
        public virtual Characteristic Characteristic { get; set; } // Navigation property to Characteristic
    }
}
